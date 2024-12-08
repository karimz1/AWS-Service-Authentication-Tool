﻿using AwsServiceAuthenticator.Cli;
using AwsServiceAuthenticator.Cli.Options;
using AwsServiceAuthenticator.Cli.Utls;
using AwsServiceAuthenticator.Commands.Enums;
using AwsServiceAuthenticator.Commands.Factory;
using AwsServiceAuthenticator.Commands.Handler;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using ILogger = AwsServiceAuthenticator.Core.Interfaces.ILogger;

ILogger? logger = null;

try
{
    var logFileName = $"refreshTokens-{DateTime.Now:yyyy-MM-dd}.log";

    var argumentParserService = new ArgumentParserService<ArgumentOptions>();
    var parserResult = argumentParserService.ParseArguments(args);

    parserResult
        .WithParsedAsync(async options =>
        {
            var logFilePath = Path.Combine(options.LogFolderPath, logFileName);
            var serviceProvider = ServiceConfiguration.ConfigureServices(options.Region, logFilePath, options.DebugMode);
            logger = serviceProvider.GetRequiredService<ILogger>();
            logger.LogInformation($"Starting with region: {options.Region}, log path: {options.LogFolderPath}");

            var commandRegistry = serviceProvider.GetRequiredService<ICommandRegistry>();
            var handler = commandRegistry.GetCommandHandler(options.Command);

            if (handler == null || options.Command == AvailableCommandType.None)
            {
                logger.LogError($"Invalid command: {options.CommandRaw}");
                return;
            }

            await handler.ExecuteAsync();
        }).Wait();

    parserResult.WithNotParsed(errors =>
    {
        var helpText = argumentParserService.GenerateHelpText(
            parserResult,
            heading: "AwsServiceAuthenticator.Cli",
            preOptionsLine: "Available commands:\n" + CommandHelper.GetAvailableCommands()
        );
        Console.WriteLine(helpText);
    });
}
catch (Exception ex)
{
    logger?.LogError($"Unhandled exception: {ex.Message}");
    logger?.LogError($"Stack trace: {ex.StackTrace}");
}
finally
{
    logger?.LogInformation("Application has exited.");
}