using GrpcServiceTests.Gateway.IntegrationTests.Setup;
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
        const string expected = "Custom Extra Service";
        Fixture.ConfigureWebHost(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IExtraService>(_ => new CustomExtraService(expected));
            });
        });

        var client = new Greeter.GreeterClient(Channel);

        // testing configuration
        var config = Fixture.TestServer.Services.GetService<IConfiguration>();
        _output.WriteLine($"Version: {config?["version"]}");

        // Act
        var response = await client.SayHelloAsync(new HelloRequest { Name = "Joe" });

        // Assert
        Assert.Equal($"Hello Joe! Result: {expected}", response.Message);
    }
}
