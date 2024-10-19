// =====================================
// Author: Jason Malley
// =====================================

namespace Malley.WebLogs.Api.Domain;

public interface IPersist
{
    public Task SaveAsync<T>(T input) where T : class;

    public IAsyncEnumerable<T> GetAsync<T>(int? count = null) where T : class;

    public Task<T> ReadObjectAsync<T>(string key) where T : class;
}