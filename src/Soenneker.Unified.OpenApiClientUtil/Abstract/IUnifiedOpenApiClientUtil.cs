using Soenneker.Unified.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Unified.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a cached Unified OpenAPI client backed by authenticated transport.
/// </summary>
public interface IUnifiedOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the client, creating it on first use.
    /// </summary>
    /// <param name="cancellationToken">The token used to cancel client creation.</param>
    /// <returns>The cached client.</returns>
    ValueTask<UnifiedOpenApiClient> Get(CancellationToken cancellationToken = default);
}
