using Grpc.Net.Client;

namespace GrpcServiceTests.Gateway.IntegrationTests.Setup;

public class TestBaseSetup : IClassFixture<GrpcTestFixture<Startup>>, IDisposable
{
    private GrpcChannel? _channel;

    protected GrpcTestFixture<Startup> Fixture { get; }

    protected GrpcChannel Channel => _channel ??= CreateChannel();
    private GrpcChannel CreateChannel()
    {
        return GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            HttpHandler = Fixture.Handler
        });
    }

    protected TestBaseSetup(GrpcTestFixture<Startup> fixture)
    {
        Fixture = fixture;
    }

    public void Dispose()
    {
        _channel = null;
    }
}
