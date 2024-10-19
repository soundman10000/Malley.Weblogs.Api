// =====================================
// Author: Jason Malley
// =====================================

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Malley.WebLogs.Api.Domain;

public static class JsonSerializationOptions
{
    public static readonly JsonSerializerOptions Instance = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}