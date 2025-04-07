using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Soenneker.Extensions.String;

namespace Soenneker.Extensions.Configuration.Logging;

/// <summary>
/// A collection of helpful IConfiguration logging related extension methods
/// </summary>
public static class ConfigurationLoggingExtension
{
    /// <summary>
    /// Retrieves the default Serilog <see cref="LogEventLevel"/> from the configuration.
    /// It first attempts to read <c>Log:Levels:Default</c>, falling back to <c>Log:DefaultLogLevel</c> if not present or invalid.
    /// If both are missing or unparsable, it defaults to <see cref="LogEventLevel.Verbose"/>.
    /// </summary>
    /// <param name="config">The configuration instance to read from.</param>
    /// <returns>The resolved <see cref="LogEventLevel"/> from the configuration, or <see cref="LogEventLevel.Verbose"/> if unavailable.</returns>
    public static LogEventLevel GetLogEventLevel(this IConfiguration config)
    {
        return config["Log:Levels:Default"].TryToEnum<LogEventLevel>()
               ?? config.GetValue<string>("Log:DefaultLogLevel").TryToEnum<LogEventLevel>()
               ?? LogEventLevel.Verbose;
    }
}
