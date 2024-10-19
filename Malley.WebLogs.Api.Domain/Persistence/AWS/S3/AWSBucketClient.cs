// =====================================
// Author: Jason Malley
// =====================================

using System.Text.Json;
using Amazon.S3;
using Amazon.S3.Model;
using LanguageExt;

namespace Malley.WebLogs.Api.Domain;

public class AWSBucketClient : IPersist
{
    private readonly AmazonS3Client _client;
    private static readonly string BucketName = "malley-sandbox";
    private static readonly int DefaultPageSize = 100;
    private static readonly string MimeType = "application/json";

    public AWSBucketClient(AmazonClientFactory factory)
    {
        this._client = factory.BuildS3Client();
    }

    public Task SaveAsync<T>(T input) where T : class => 
        Serialize(input)
            .Apply(this.SendAsync);

    public async IAsyncEnumerable<T> GetAsync<T>(int? count = null) where T : class
    {
        string? continuationToken = null;
        var request = new ListObjectsV2Request
        {
            BucketName = BucketName,
            MaxKeys = count ?? DefaultPageSize
        };

        do
        {
            if (continuationToken != null)
            {
                request.ContinuationToken = continuationToken;
            }

            var response = await this._client.ListObjectsV2Async(request);
            foreach (var s3Object in response.S3Objects)
            {
                yield return await this.ReadObjectAsync<T>(s3Object.Key);
            }
            
            continuationToken = response.NextContinuationToken;

            if (response.S3Objects.Count == 0 && string.IsNullOrEmpty(continuationToken))
            {
                break;
            }
        } while (!string.IsNullOrEmpty(continuationToken));
    }

    public async Task<T> ReadObjectAsync<T>(string key) where T : class
    {
        var getObjectRequest = new GetObjectRequest
        {
            BucketName = BucketName,
            Key = key
        };

        try
        {
            using var response = await this._client.GetObjectAsync(getObjectRequest);
            using var streamReader = new StreamReader(response.ResponseStream);

            return await streamReader
                .ReadToEndAsync()
                .Map(Deserialize<T>) ?? throw new Exception("Could not read");
        }
        catch (Exception ex)
        {
            throw new Exception("Could not get object", ex);
        }
    }
    
    private async Task SendAsync(string input)
    {
        var key = DateTime.Now.Ticks.ToString();

        using var memoryStream = new MemoryStream();
        await using var streamWriter = new StreamWriter(memoryStream);
        
        await streamWriter.WriteAsync(input);
        await streamWriter.FlushAsync();
        memoryStream.Position = 0;
        
        var payload = new PutObjectRequest
        {
            BucketName = BucketName,
            Key = key,
            InputStream = memoryStream,
            ContentType = MimeType
        };

        try
        {
            var resp = await this._client.PutObjectAsync(payload);

            if (!((int)resp.HttpStatusCode >= 200 && (int)resp.HttpStatusCode < 300))
            {
                throw new Exception($"Failed to upload object. Status Code: {resp.HttpStatusCode}");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Was unable to send data to amazon s3 bucket", e);
        }
    }

    private static string Serialize<T>(T instance) where T : class
    {
        try
        {
            return JsonSerializer.Serialize(instance, JsonSerializationOptions.Instance);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to serialize type {typeof(T)}", ex);
        }
    }

    private static T Deserialize<T>(string json) where T : class
    {
        try
        {
            return JsonSerializer.Deserialize<T>(json, JsonSerializationOptions.Instance)
                   ?? throw new InvalidOperationException();
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to serialize type {typeof(T)}", ex);
        }
    }
}