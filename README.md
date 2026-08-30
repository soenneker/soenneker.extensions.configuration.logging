[![](https://img.shields.io/nuget/v/soenneker.extensions.configuration.logging.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.configuration.logging/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.configuration.logging/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.configuration.logging/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.configuration.logging.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.configuration.logging/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.configuration.logging/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.configuration.logging/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Configuration.Logging

Resolves a Serilog `LogEventLevel` from `IConfiguration` using a preferred key and a backward-compatible legacy key.

## Installation

```bash
dotnet add package Soenneker.Extensions.Configuration.Logging
```

## Configuration

Use `Log:Levels:Default` for new applications:

```json
{
  "Log": {
    "Levels": {
      "Default": "Information"
    }
  }
}
```

`Log:DefaultLogLevel` remains supported for applications using the earlier configuration shape:

```json
{
  "Log": {
    "DefaultLogLevel": "Warning"
  }
}
```

Supported values are `Verbose`, `Debug`, `Information`, `Warning`, `Error`, and `Fatal`. Matching is case-insensitive.

## Usage

```csharp
using Soenneker.Extensions.Configuration.Logging;
using Serilog;

var level = configuration.GetLogEventLevel();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Is(level)
    .WriteTo.Console()
    .CreateLogger();
```

Resolution is deterministic:

1. `Log:Levels:Default` is used when present.
2. Otherwise, `Log:DefaultLogLevel` is used when present.
3. When neither key exists, the result is `Information`.

An explicitly configured but unsupported value throws `InvalidOperationException`. This prevents a typo in the preferred key from being hidden by the legacy key or an unexpectedly permissive fallback.

The extension only resolves the level. It does not create a logger, add sinks, reload configuration, or change Serilog's global logger.
