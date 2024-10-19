// =====================================
// Author: Jason Malley
// =====================================

using Microsoft.Extensions.DependencyInjection;

namespace Malley.WebLogs.Api.Domain;

public static class ApplicationRegistrar
{
    public static IServiceCollection AddMalleyWebApiApplication(this IServiceCollection services)
    {
        services.AddBuilders();
        services.AddPersistence();
        return services;
    }
}