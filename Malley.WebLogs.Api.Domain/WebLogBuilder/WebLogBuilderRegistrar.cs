// =====================================
// Author: Jason Malley
// =====================================

using Microsoft.Extensions.DependencyInjection;

namespace Malley.WebLogs.Api.Domain;

public static class WebLogBuilderRegistrar
{
    public static IServiceCollection AddBuilders(this IServiceCollection services)
    {
        var appTypes = typeof(WebLogBuilderRegistrar).Assembly.GetTypes();
        var builderType = typeof(IWebLogBuilder<>);
        foreach (var implementation in builderType.GetImplementations())
        {
            var implementationType = appTypes
                .FirstOrDefault(t => t.GetInterfaces().Contains(implementation) && !t.IsAbstract);

            if (implementationType == null)
            {
                continue;
            }

            services.AddSingleton(implementation, implementationType);
        }

        return services;
    }
}