using GrpcServiceTests.Gateway.IntegrationTests.Setup;
using Xunit.Abstractions;

namespace GrpcServiceTests.Gateway.IntegrationTests;

public class Tests : TestBaseSetup
{
    private readonly ITestOutputHelper _output;

    public Tests(GrpcTestFixture<Startup> fixture, ITestOutputHelper output) : base(fixture)
    {
        _output = output;
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task LoadTest(string number)
    {
        // Arrange
        var expected = $"Mock Extra Service {number}";
        Fixture.ExtraServiceMock.Setup(x => x.Do()).Returns(expected);

        var client = new Greeter.GreeterClient(Channel);

        // Act
        var response = await client.SayHelloAsync(new HelloRequest { Name = "Joe" });

        // Assert
        _output.WriteLine(response.Message);
        Assert.Equal($"Hello Joe! Result: {expected}", response.Message);
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            for (var i = 0; i < 100; i++)
            {
                yield return new object[] { i.ToString() };
            }
        }
    }
}
