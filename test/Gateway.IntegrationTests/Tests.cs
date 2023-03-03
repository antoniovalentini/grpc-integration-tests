using GrpcServiceTests.Gateway.Services;

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
        // var client = new greet
        //
        // // Act
        // var response = await client.SayHelloUnaryAsync(new HelloRequest { Name = "Joe" });
        //
        // // Assert
        // Assert.Equal("Hello Joe", response.Message);
    }
}
