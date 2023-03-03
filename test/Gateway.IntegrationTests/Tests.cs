using System.Reflection;
using GrpcServiceTests.Gateway.IntegrationTests.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace GrpcServiceTests.Gateway.IntegrationTests;

public class Tests : TestBaseSetup
{
    private readonly ITestOutputHelper _output;

    public Tests(GrpcTestFixture<Startup> fixture, ITestOutputHelper output) : base(fixture)
    {
        _output = output;
    }

    [Fact]
    public async Task SayHelloUnaryTest()
    {
        // Arrange
        Fixture.ConfigureWebHost(builder =>
        {
            builder.UseConfiguration(LoadTestConfiguration());
        });
        const string expected = "Real Extra Service";

        var client = new Greeter.GreeterClient(Channel);
        var config = Fixture.TestServer.Services.GetService<IConfiguration>();
        _output.WriteLine($"Version: {config?["version"]}");

        // Act
        var response = await client.SayHelloAsync(new HelloRequest { Name = "Joe" });

        // Assert
        Assert.Equal($"Hello Joe! Result: {expected}", response.Message);
    }

    private static IConfiguration LoadTestConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new Exception("Unable to set config base path."))
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }
}
