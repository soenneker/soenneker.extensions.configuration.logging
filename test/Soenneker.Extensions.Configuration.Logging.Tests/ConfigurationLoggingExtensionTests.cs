using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog.Events;

namespace Soenneker.Extensions.Configuration.Logging.Tests;

public sealed class ConfigurationLoggingExtensionTests
{
    [Test]
    public async Task Missing_configuration_defaults_to_information()
    {
        IConfiguration configuration = new ConfigurationBuilder().Build();

        await Assert.That(configuration.GetLogEventLevel()).IsEqualTo(LogEventLevel.Information);
    }

    [Test]
    public async Task Legacy_configuration_is_supported_case_insensitively()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["Log:DefaultLogLevel"] = "warning" })
            .Build();

        await Assert.That(configuration.GetLogEventLevel()).IsEqualTo(LogEventLevel.Warning);
    }

    [Test]
    public void Invalid_primary_configuration_does_not_fall_back_to_legacy_value()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Log:Levels:Default"] = "Info",
                ["Log:DefaultLogLevel"] = "Warning"
            })
            .Build();

        Assert.Throws<InvalidOperationException>(() => configuration.GetLogEventLevel());
    }
}
