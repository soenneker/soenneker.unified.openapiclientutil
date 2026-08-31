[![](https://img.shields.io/nuget/v/soenneker.unified.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.unified.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.unified.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.unified.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.unified.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.unified.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.unified.openapiclientutil/codeql.yml?style=for-the-badge&label=CodeQL)](https://github.com/soenneker/soenneker.unified.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Unified.OpenApiClientUtil

Provides a cached `UnifiedOpenApiClient` backed by an authenticated Unified HTTP client.

## Installation

```bash
dotnet add package Soenneker.Unified.OpenApiClientUtil
```

## Configuration

```json
{
  "Unified": {
    "ApiKey": "your-workspace-api-token"
  }
}
```

The underlying transport sends `Authorization: Bearer <ApiKey>`. For EU or Australian workspaces, set `Unified:ClientBaseUrl` to `https://api-eu.unified.to/` or `https://api-au.unified.to/`.

## Registration

```csharp
using Soenneker.Unified.OpenApiClientUtil.Registrars;

services.AddUnifiedOpenApiClientUtilAsScoped();
```

Use `AddUnifiedOpenApiClientUtilAsSingleton()` to share the generated client wrapper too. Both registrations borrow the singleton HTTP provider; disposing a scoped wrapper does not remove its transport.

## Usage

```csharp
using Soenneker.Unified.OpenApiClient;
using Soenneker.Unified.OpenApiClient.Models;
using Soenneker.Unified.OpenApiClientUtil.Abstract;

public sealed class ConnectionReader
{
    private readonly IUnifiedOpenApiClientUtil _clients;

    public ConnectionReader(IUnifiedOpenApiClientUtil clients)
    {
        _clients = clients;
    }

    public async ValueTask<List<Connection>?> GetSandboxConnections(
        CancellationToken cancellationToken)
    {
        UnifiedOpenApiClient client = await _clients.Get(cancellationToken);

        return await client.Unified.Connection.GetAsync(
            request => request.QueryParameters.Env = "Sandbox",
            cancellationToken);
    }
}
```

`Get()` initializes the generated client once for that provider instance and returns the same instance afterward. Unified and transport failures are propagated through Kiota exceptions.
