using System;
using Microsoft.Extensions.Configuration;
using Serilog.Events;

namespace Soenneker.Extensions.Configuration.Logging;

/// <summary>
/// Provides configuration-based Serilog log-level resolution.
/// </summary>
public static class ConfigurationLoggingExtension
{
    /// <summary>
    /// Resolves the default Serilog <see cref="LogEventLevel"/> from configuration.
    /// <c>Log:Levels:Default</c> takes precedence over the legacy <c>Log:DefaultLogLevel</c> key.
    /// </summary>
    /// <param name="config">The configuration instance to read from.</param>
    /// <returns>The configured level, or <see cref="LogEventLevel.Information"/> when neither key is configured.</returns>
    /// <exception cref="InvalidOperationException">The configured value is not a supported Serilog log level.</exception>
    public static LogEventLevel GetLogEventLevel(this IConfiguration config)
    {
        const string primaryKey = "Log:Levels:Default";
        string? configured = config[primaryKey];

        if (configured is not null)
            return ParseLevel(primaryKey, configured);

        const string legacyKey = "Log:DefaultLogLevel";
        configured = config[legacyKey];

        return configured is null ? LogEventLevel.Information : ParseLevel(legacyKey, configured);
    }

    private static LogEventLevel ParseLevel(string key, string configured)
    {
        if (Enum.TryParse(configured, true, out LogEventLevel level) && Enum.IsDefined(level))
            return level;

        throw new InvalidOperationException($"Configuration key '{key}' is not a supported Serilog log level.");
    }
}
