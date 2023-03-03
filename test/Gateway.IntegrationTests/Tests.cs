using GrpcServiceTests.Gateway.IntegrationTests.Setup;

namespace GrpcServiceTests.Gateway.IntegrationTests;

public class Tests : TestBaseSetup
{
    public Tests(GrpcTestFixture<Startup> fixture) : base(fixture)
    {

    }

    [Fact]
    public async Task SayHelloUnaryTest()
    {
        // Arrange
        var client = new Greeter.GreeterClient(Channel);

        // Act
        var response = await client.SayHelloAsync(new HelloRequest { Name = "Joe" });

        // Assert
        Assert.Equal("Hello Joe", response.Message);
    }
}
