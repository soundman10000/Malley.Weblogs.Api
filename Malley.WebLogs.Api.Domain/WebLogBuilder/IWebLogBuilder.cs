// =====================================
// Author: Jason Malley
// =====================================

namespace Malley.WebLogs.Api.Domain;

public interface IWebLogBuilder<T> where T : class
{
    public WebApplications Application { get; }

    public Task<T> Build();
    
    public Task<IEnumerable<MicrowaveCookLog>> Build(int count);
}