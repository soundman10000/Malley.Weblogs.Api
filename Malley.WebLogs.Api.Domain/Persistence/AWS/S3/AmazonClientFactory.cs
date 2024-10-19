// =====================================
// Author: Jason Malley
// =====================================

using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using LanguageExt;

namespace Malley.WebLogs.Api.Domain
{
    public class AmazonClientFactory
    {
        private static readonly string AccessKeyId = string.Empty;
        private static readonly string Secret = string.Empty;

        public AmazonS3Client BuildS3Client() =>
            new BasicAWSCredentials(AccessKeyId, Secret)
                .Apply(x => new AmazonS3Client(x, RegionEndpoint.USEast2));
    }
}
