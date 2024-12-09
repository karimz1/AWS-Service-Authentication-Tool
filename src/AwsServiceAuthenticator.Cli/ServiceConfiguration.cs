using System.Diagnostics;
using Amazon;
using AwsServiceAuthenticator.Commands.Enums;
using AwsServiceAuthenticator.Commands.Factory;
using AwsServiceAuthenticator.Commands.Handler;
using AwsServiceAuthenticator.Core.Interfaces;
using AwsServiceAuthenticator.Core.Models;
using AwsServiceAuthenticator.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using ILogger = AwsServiceAuthenticator.Core.Interfaces.ILogger;

namespace AwsServiceAuthenticator.Cli;

public static class ServiceConfiguration
{
    public static ServiceProvider ConfigureServices(string region, string logFilePath, bool debugMode)
    {
        var serviceConfiguration = new ServiceCollection();
        
        
        serviceConfiguration.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(dispose: true);
        });
        
        var serviceProvider = serviceConfiguration.RegisterCommands()
                                                  .RegisterAwsServices(region)
                                                  .RegisterLogging(logFilePath, debugMode)
                                                  .BuildServiceProvider();
        
        if (debugMode)
            ConfigureAwsLogging(serviceProvider.GetRequiredService<ILogger>());
        
        return serviceProvider;
        
        
    }

    private static void ConfigureAwsLogging(ILogger logger)
    {
        var listener = new SerilogTraceRedirect(logger);
        AWSConfigs.AddTraceListener("Amazon", listener);
        AWSConfigs.LoggingConfig.LogTo = LoggingOptions.SystemDiagnostics;
        AWSConfigs.LoggingConfig.LogMetrics = true;
        AWSConfigs.LoggingConfig.LogResponses = ResponseLoggingOption.Always;
        AWSConfigs.LoggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
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

    private static IServiceCollection RegisterAwsServices(this IServiceCollection services, string region)
    {
        services.AddSingleton<IAwsAuthenticator, AwsAuthenticator>();
        services.AddSingleton<ISystemRegion>(_ => new SystemRegion(region));
        return services;
    }

    private static IServiceCollection RegisterLogging(this IServiceCollection services, string logFilePath, bool debugMode)
    {
        var levelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = debugMode ? LogEventLevel.Debug :
                LogEventLevel.Information
        };

        var logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(levelSwitch).Enrich.FromLogContext()
            .WriteTo.Trace()
            .WriteTo.Console()
            .WriteTo.File(path: logFilePath, 
                          fileSizeLimitBytes: 5 * 1024 * 1024, // 5 MB
                          rollOnFileSizeLimit: true)
            .CreateLogger();

        services.AddSingleton<ILogger, SerilogLogger>(_ => new SerilogLogger(logger));
        return services;
    }
}
