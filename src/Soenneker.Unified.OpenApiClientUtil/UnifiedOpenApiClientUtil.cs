using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Unified.HttpClients.Abstract;
using Soenneker.Unified.OpenApiClientUtil.Abstract;
using Soenneker.Unified.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Unified.OpenApiClientUtil;

///<inheritdoc cref="IUnifiedOpenApiClientUtil"/>
public sealed class UnifiedOpenApiClientUtil : IUnifiedOpenApiClientUtil
{
    private readonly AsyncSingleton<UnifiedOpenApiClient> _client;

    public UnifiedOpenApiClientUtil(IUnifiedOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<UnifiedOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Unified:ApiKey");
            string authHeaderValueTemplate = configuration["Unified:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

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
