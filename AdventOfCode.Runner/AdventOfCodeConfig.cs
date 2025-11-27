using Microsoft.Extensions.Configuration;
using System;

namespace AdventOfCode.Runner;

public static class AocConfig
{
    private static readonly Lazy<IConfigurationRoot> _config = new(() =>
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables();

        return builder.Build();
    });

    public static string SessionCookie =>
        _config.Value["AdventOfCodeSessionCookie"]
        ?? throw new InvalidOperationException("AdventOfCodeSessionCookie is missing from user secrets");
}
