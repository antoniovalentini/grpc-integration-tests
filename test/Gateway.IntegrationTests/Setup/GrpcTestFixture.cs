using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;

namespace GrpcServiceTests.Gateway.IntegrationTests.Setup;

public class GrpcTestFixture<TStartup> : IDisposable where TStartup : class
{
    private TestServer? _server;
    private IHost? _host;
    private HttpMessageHandler? _handler;

    public readonly Mock<IExtraService> ExtraServiceMock = new();

    private void EnsureServer()
    {
        if (_host != null) return;

        var builder = new HostBuilder()
            .ConfigureServices(_ => { })
            .ConfigureWebHostDefaults(webHost =>
            {
                webHost
                    .UseConfiguration(LoadTestConfiguration())
                    .UseTestServer()
                    .UseStartup<TStartup>();

                webHost.ConfigureServices(services =>
                {
                    services.AddScoped<IExtraService>(_ => ExtraServiceMock.Object);
                });

                // webHost.ConfigureTestServices(services =>
                // {
                //     services.AddScoped<IExtraService>(_ => ExtraServiceMock.Object);
                // });
            });

        _host = builder.Start();
        _server = _host.GetTestServer();
        _handler = _server.CreateHandler();
    }

    public HttpMessageHandler Handler
    {
        get
        {
            EnsureServer();
            return _handler!;
        }
    }

    public void Dispose()
    {
        _handler?.Dispose();
        _host?.Dispose();
        _server?.Dispose();
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
