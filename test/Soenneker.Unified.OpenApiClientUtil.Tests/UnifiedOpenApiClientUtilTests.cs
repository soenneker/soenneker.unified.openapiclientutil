using Soenneker.Unified.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Unified.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class UnifiedOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IUnifiedOpenApiClientUtil _openapiclientutil;

    public UnifiedOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IUnifiedOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
