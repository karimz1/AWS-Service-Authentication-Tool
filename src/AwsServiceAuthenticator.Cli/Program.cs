using AwsServiceAuthenticator.Cli;
using AwsServiceAuthenticator.Core.Interfaces;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;

ILogger logger = null;

try
{
    ServiceConfiguration.ConfigureAwsLogging();
    ServiceConfiguration.InitializeTraceListeners();
    var logFileName = $"refreshTokens-{DateTime.Now:yyyy-MM-dd}.log";

    var result = await Parser.Default.ParseArguments<Options>(args)
        .WithParsedAsync(async options =>
        {
            logFileName = Path.Combine(options.LogFolderPath, logFileName);
            var serviceProvider = ServiceConfiguration.ConfigureServices(options.Region, logFileName);
            logger = serviceProvider.GetRequiredService<ILogger>();

            var region = options.Region;
            var logPath = options.LogFolderPath;

            logger.LogInformation($"Starting with region: {region}, log path: {logPath}");

            var command = serviceProvider.GetRequiredService<ICommandResolver>()
                .ResolveCommand(options.Command.ToLower());

            if (command == null)
            {
                logger.LogError($"Invalid command: {options.Command}");
                return;
            }

            await command.ExecuteAsync();
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