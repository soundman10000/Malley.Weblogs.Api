// =====================================
// Author: Jason Malley
// =====================================

using Bogus;
using LanguageExt;

namespace Malley.WebLogs.Api.Domain;

public class MicroWaveWebLogBuilder : IWebLogBuilder<MicrowaveCookLog>
{
    private static readonly Faker<MicrowaveCookLog> Rng = FakerFactory();
    private static readonly int TaskLimit = 25;

    public WebApplications Application => WebApplications.MircoWave;
    
    public Task<MicrowaveCookLog> Build() => Rng.Generate().AsTask();

    public Task<IEnumerable<MicrowaveCookLog>> Build(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Count must be greater than to zero.");
        }

        return Enumerable.Range(0, count)
            .Select(_ => this.Build())
            .TraverseParallel(TaskLimit, x => x);
    }

    private static Faker<MicrowaveCookLog> FakerFactory() =>
        new Faker<MicrowaveCookLog>()
            .RuleFor(z => z.CookTime, f => f.RandomTimeSpan(15, 1000))
            .RuleFor(z => z.TemperatureInFahrenheit, f => f.RandomTemperature(100, 220))
            .RuleFor(z => z.UserId, f => f.Internet.UserName());
}