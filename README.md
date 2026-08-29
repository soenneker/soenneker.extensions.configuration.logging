[![](https://img.shields.io/nuget/v/soenneker.extensions.configuration.logging.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.configuration.logging/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.configuration.logging/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.configuration.logging/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.configuration.logging.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.configuration.logging/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.configuration.logging/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.configuration.logging/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Configuration.Logging

A collection of helpful IConfiguration logging related extension methods.

## Installation

```bash
dotnet add package Soenneker.Extensions.Configuration.Logging
```

## Quick start

```csharp
using Soenneker.Extensions.Configuration.Logging;

// Given an existing IConfiguration named config:
var result = config.GetLogEventLevel();
```

## Common operations

- `GetLogEventLevel()` - Retrieves the default Serilog `LogEventLevel` from the configuration. It first attempts to read `Log:Levels:Default`, falling back to `Log:DefaultLogLevel` if not present or invalid. If both are missing or unparsable, it defaults to `LogEventLevel.Verbose`.
