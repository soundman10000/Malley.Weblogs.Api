// =====================================
// Author: Jason Malley
// =====================================

using Microsoft.Extensions.DependencyInjection;

namespace Malley.WebLogs.Api.Domain;

public static class AmazonRegistrar
{
    public static IServiceCollection AddAmazonS3(this IServiceCollection services)
    {
        services
            .AddSingleton<IPersist, AWSBucketClient>()
            .AddSingleton<AmazonClientFactory>();

        return services;
    }
}