// =====================================
// Author: Jason Malley
// =====================================

namespace Malley.WebLogs.Api.Domain;

public record MicrowaveCookLog
{
    public required string UserId { get; init; }
    public required TimeSpan CookTime { get; init; }
    public int TemperatureInFahrenheit { get; init; }
}