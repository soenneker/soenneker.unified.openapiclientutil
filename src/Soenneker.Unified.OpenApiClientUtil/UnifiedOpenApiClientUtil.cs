using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Unified.HttpClients.Abstract;
using Soenneker.Unified.OpenApiClientUtil.Abstract;
using Soenneker.Unified.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Unified.OpenApiClientUtil;

/// <inheritdoc cref="IUnifiedOpenApiClientUtil" />
public sealed class UnifiedOpenApiClientUtil : IUnifiedOpenApiClientUtil
{
    private readonly AsyncSingleton<UnifiedOpenApiClient> _client;

    public UnifiedOpenApiClientUtil(IUnifiedOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<UnifiedOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new UnifiedOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<UnifiedOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
