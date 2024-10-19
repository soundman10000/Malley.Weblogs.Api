// =====================================
// Author: Jason Malley
// =====================================

using LanguageExt;

namespace Malley.WebLogs.Api.Domain;

public static class TaskExtensions
{
    public static async Task<Unit> MapToUnitAsync<T>(this Task<T> task, Func<T, Task> asyncAction)
    {
        var result = await task;
        await asyncAction(result);
        return Unit.Default;
    }
}