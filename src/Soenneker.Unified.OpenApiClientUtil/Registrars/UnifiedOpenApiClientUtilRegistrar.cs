using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Unified.HttpClients.Registrars;
using Soenneker.Unified.OpenApiClientUtil.Abstract;

namespace Soenneker.Unified.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class UnifiedOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="UnifiedOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddUnifiedOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddUnifiedOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IUnifiedOpenApiClientUtil, UnifiedOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="UnifiedOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddUnifiedOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddUnifiedOpenApiHttpClientAsSingleton()
                .TryAddScoped<IUnifiedOpenApiClientUtil, UnifiedOpenApiClientUtil>();

        return services;
    }
}
