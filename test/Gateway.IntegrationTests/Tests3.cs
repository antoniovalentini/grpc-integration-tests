using GrpcServiceTests.Gateway.IntegrationTests.Setup;
using Xunit.Abstractions;

namespace GrpcServiceTests.Gateway.IntegrationTests;

public class Tests3 : TestBaseSetup
{
    private readonly ITestOutputHelper _output;

    public Tests3(GrpcTestFixture<Startup> fixture, ITestOutputHelper output) : base(fixture)
    {
        _output = output;
    }

    [Theory]
    [MemberData(nameof(SharedTestData.TestData), MemberType = typeof(SharedTestData))]
    public async Task LoadTest(string number)
    {
        // Arrange
        var expected = $"Test3 Extra Service {number}";
        Fixture.ExtraServiceMock.Setup(x => x.Do()).Returns(expected);

        var client = new Greeter.GreeterClient(Channel);

        // Act
        var response = await client.SayHelloAsync(new HelloRequest { Name = "Joe" });

        // Assert
        _output.WriteLine(response.Message);
        Assert.Equal($"Hello Joe! Result: {expected}", response.Message);
    }
}
