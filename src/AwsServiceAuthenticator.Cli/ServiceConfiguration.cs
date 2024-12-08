using System.Diagnostics;
using Amazon;
using AwsServiceAuthenticator.Commands.Enums;
using AwsServiceAuthenticator.Commands.Factory;
using AwsServiceAuthenticator.Commands.Handler;
using AwsServiceAuthenticator.Core.Interfaces;
using AwsServiceAuthenticator.Core.Models;
using AwsServiceAuthenticator.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ILogger = AwsServiceAuthenticator.Core.Interfaces.ILogger;

namespace AwsServiceAuthenticator.Cli;

public static class ServiceConfiguration
{
    public static ServiceProvider ConfigureServices(string region, string logFilePath)
    {
        return new ServiceCollection().RegisterCommands()
                                      .RegsiterAwsServices(region)
                                      .RegisterLogging(logFilePath).BuildServiceProvider();
    }

    public static void ConfigureAwsLogging()
    {
        AWSConfigs.LoggingConfig.LogTo = LoggingOptions.Console;
        AWSConfigs.LoggingConfig.LogMetrics = true;
        AWSConfigs.LoggingConfig.LogResponses = ResponseLoggingOption.Always;
        AWSConfigs.LoggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
    }

    public static void InitializeTraceListeners()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    }

    private static IServiceCollection RegisterCommands(this IServiceCollection services)
    {
        services.AddTransient<NuGetAuthCommandHandler>();
        services.AddTransient<EcrAuthCommandHandler>();
        
        services.AddSingleton<ICommandRegistry>(provider =>
        {
            var registry = new CommandRegistry(provider);
            registry.MapHandlerToAvailableCommandTypeFor<NuGetAuthCommandHandler>(AvailableCommandType.NuGet);
            registry.MapHandlerToAvailableCommandTypeFor<EcrAuthCommandHandler>(AvailableCommandType.Ecr);
            return registry;
        });
        return services;
    }

    private static IServiceCollection RegsiterAwsServices(this IServiceCollection services, string region)
    {
        services.AddSingleton<IAwsAuthenticator, AwsAuthenticator>();
        services.AddSingleton<ISystemRegion>(_ => new SystemRegion(region));
        return services;
    }

    private static IServiceCollection RegisterLogging(this IServiceCollection services, string logFilePath)
    {
        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(logFilePath)
            .CreateLogger();

        services.AddSingleton<ILogger, SerilogLogger>(_ => new SerilogLogger(logger));
        return services;
    }
}
