namespace GrpcServiceTests.Gateway.IntegrationTests;

public static class SharedTestData
{
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
