using Grpc.Core;

namespace GrpcServiceTests.Gateway.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly IExtraService _extraService;

    public GreeterService(IExtraService extraService)
    {
        _extraService = extraService;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = $"Hello {request.Name}! Result: {_extraService.Do()}"
        });
    }
}
