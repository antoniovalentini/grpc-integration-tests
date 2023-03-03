namespace GrpcServiceTests.Gateway;

public interface IExtraService
{
    public string Do();
}

public class RealExtraService : IExtraService
{
    public string Do() => "Real Extra Service";
}
