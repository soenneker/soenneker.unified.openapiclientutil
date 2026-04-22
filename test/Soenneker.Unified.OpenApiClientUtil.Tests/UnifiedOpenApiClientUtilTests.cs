using Soenneker.Unified.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Unified.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class UnifiedOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IUnifiedOpenApiClientUtil _openapiclientutil;

    public UnifiedOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IUnifiedOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
