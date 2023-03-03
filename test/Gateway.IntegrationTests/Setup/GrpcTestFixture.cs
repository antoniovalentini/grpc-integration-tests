using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace GrpcServiceTests.Gateway.IntegrationTests.Setup;

public class GrpcTestFixture<TStartup> : IDisposable where TStartup : class
{
    private TestServer? _server;
    private IHost? _host;
    private HttpMessageHandler? _handler;
    private Action<IWebHostBuilder>? _configureWebHost;

    public void ConfigureWebHost(Action<IWebHostBuilder> configure)
    {
        _configureWebHost = configure;
    }

    private void EnsureServer()
    {
        if (_host != null) return;

        var builder = new HostBuilder()
            .ConfigureServices(_ =>
            {
                // services.AddSingleton<ILoggerFactory>(LoggerFactory);
            })
            .ConfigureWebHostDefaults(webHost =>
            {
                webHost
                    .UseTestServer()
                    .UseStartup<TStartup>();

                _configureWebHost?.Invoke(webHost);
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

    public TestServer TestServer
    {
        get
        {
            EnsureServer();
            return _server!;
        }
    }

    public void Dispose()
    {
        _handler?.Dispose();
        _host?.Dispose();
        _server?.Dispose();
    }
}
