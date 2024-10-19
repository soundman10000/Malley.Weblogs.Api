// =====================================
// Author: Jason Malley
// =====================================

using Microsoft.Extensions.DependencyInjection;

namespace Malley.WebLogs.Api.Domain;

public static class PersistenceRegistrar
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddAmazonS3();
        return services;
    }
}