// =====================================
// Author: Jason Malley
// =====================================

using Malley.WebLogs.Api.Domain;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Malley.WebLogs.Api.Tests;

[Collection(TestConstants.Collection)]
public class Sandbox
{
    private readonly ITestOutputHelper _output;
    private readonly IWebLogBuilder<MicrowaveCookLog> _microwaveBuilder;
    private readonly IPersist _s3Client;

    public Sandbox(TestFixtureServiceFactory fixture, ITestOutputHelper output)
    {
        this._output = output;
        this._microwaveBuilder = fixture.Services.GetRequiredService<IWebLogBuilder<MicrowaveCookLog>>();
        this._s3Client = fixture.Services.GetRequiredService<IPersist>();
    }

    [Fact]
    public async Task Thing()
    {
        //await this._microwaveBuilder
        //    .Build()
        //    .MapToUnitAsync(this._s3Client.SaveAsync);

        var objects = await this._s3Client
            .GetAsync<MicrowaveCookLog>()
            .ToListAsync();

        var here = 1;
    }
}