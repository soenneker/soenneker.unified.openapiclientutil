using Soenneker.Unified.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Unified.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IUnifiedOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<UnifiedOpenApiClient> Get(CancellationToken cancellationToken = default);
}
