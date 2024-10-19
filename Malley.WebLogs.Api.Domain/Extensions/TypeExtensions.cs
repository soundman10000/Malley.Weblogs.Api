// =====================================
// Author: Jason Malley
// =====================================

namespace Malley.WebLogs.Api.Domain;

public static class TypeExtensions
{
    public static IEnumerable<Type> GetImplementations(this Type type) =>
        typeof(TypeExtensions)
            .Assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces())
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == type)
            .Distinct();
}