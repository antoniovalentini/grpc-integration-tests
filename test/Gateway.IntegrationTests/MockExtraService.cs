namespace GrpcServiceTests.Gateway.IntegrationTests;

public class MockExtraService : IExtraService
{
    private readonly string _msg;

    public MockExtraService(string msg)
    {
        _msg = msg;
    }

    public string Do() => _msg;
}
