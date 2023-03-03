using Grpc.Net.Client;

namespace GrpcServiceTests.Gateway.IntegrationTests.Setup;

public class TestBaseSetup : IClassFixture<GrpcTestFixture<Startup>>, IDisposable
{
    private GrpcChannel? _channel;

    protected GrpcTestFixture<Startup> Fixture { get; set; }


    protected GrpcChannel Channel => _channel ??= CreateChannel();

    protected GrpcChannel CreateChannel()
    {
        return GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            HttpHandler = Fixture.Handler
        });
    }

    public TestBaseSetup(GrpcTestFixture<Startup> fixture)
    {
        Fixture = fixture;
    }

    public void Dispose()
    {
        _channel = null;
    }
}
