using System.Diagnostics;
using Amazon;
using AwsServiceAuthenticator.Commands;
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
        var serviceCollection = new ServiceCollection();
        serviceCollection.RegisterLogging(logFilePath);
        serviceCollection.RegisterCommandsMapping();
       
        serviceCollection.AddSingleton<IAwsAuthenticator, AwsAuthenticator>();
        serviceCollection.AddSingleton<ISystemRegion>(_ => new SystemRegion(region));

        return serviceCollection.BuildServiceProvider();
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

    private static IServiceCollection RegisterLogging(this IServiceCollection services, string logFilePath)
    {
        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(logFilePath)
            .CreateLogger();
        
        services.AddSingleton<ILogger, SerilogLogger>(_ => new SerilogLogger(logger));
        return services;
    }

    private static IServiceCollection RegisterCommandsMapping(this IServiceCollection services)
    {
        services.AddSingleton<ICommandResolver, CommandResolver>();
        var commandMappings = CommandConfiguration.GetCommandMappings();
        
        foreach (var command in commandMappings)
            services.AddTransient(command.Value);
        
        return services;
    }
}
