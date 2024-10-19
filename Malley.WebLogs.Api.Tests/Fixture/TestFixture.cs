// =====================================
// Author: Jason Malley
// =====================================

using Malley.WebLogs.Api.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Malley.WebLogs.Api.Tests;

public static class TestConstants
{
    public const string Collection = "Tests";
}

[CollectionDefinition(TestConstants.Collection)]
public class TestCollectionFixture : ICollectionFixture<TestFixtureServiceFactory>;

public class TestFixtureServiceFactory
{
    private readonly IMessageSink _message;

    public IServiceProvider Services { get; }

    public TestFixtureServiceFactory(IMessageSink message)
    {
        this._message = message;
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(ConfigureTestApplication)
            .ConfigureServices(this.ConfigureServices)
            .Build();

        host.Start();

        this.Services = host.Services;
    }
    
    private void ConfigureServices(IServiceCollection services) =>
        services
            .AddMalleyWebApiApplication();

    private static void ConfigureTestApplication(
        HostBuilderContext context,
        IConfigurationBuilder config)
    {
        var name = Environment.GetEnvironmentVariable("ENVIRONMENT");

        config
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile($"appsettings.{name}.json", true);
    }


}