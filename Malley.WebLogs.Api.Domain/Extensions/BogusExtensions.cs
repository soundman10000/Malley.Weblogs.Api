// =====================================
// Author: Jason Malley
// =====================================

using Bogus;
using LanguageExt;

namespace Malley.WebLogs.Api.Domain;

public static class BogusExtensions
{
    public static TimeSpan RandomTimeSpan(this Faker f, int min = 0, int max = 10000)
    {
        if (min < 0 || max < 0)
        {
            throw new ArgumentException("Min and Max must be non-negative numbers.");
        }

        if (max < min)
        {
            throw new ArgumentException("Max must be greater than or equal to Min.");
        }

        return f.Random.Int(min, max).Apply(z => TimeSpan.FromSeconds(z));
    }

    public static int RandomTemperature(this Faker f, int min = 0, int max = 10000)
    {
        if (min < 0 || max < 0)
        {
            throw new ArgumentException("Min and Max must be non-negative numbers.");
        }

        if (max < min)
        {
            throw new ArgumentException("Max must be greater than or equal to Min.");
        }

        return f.Random.Int(min, max);
    }
}