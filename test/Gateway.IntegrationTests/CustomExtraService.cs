namespace GrpcServiceTests.Gateway.IntegrationTests;

public class CustomExtraService : IExtraService
{
    private readonly string _msg;

    public CustomExtraService(string msg)
    {
        _msg = msg;
    }

    public string Do() => _msg;
}
