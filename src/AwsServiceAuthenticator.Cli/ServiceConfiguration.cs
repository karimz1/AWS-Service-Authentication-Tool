using System.Diagnostics;
using Amazon;
using AwsTokenRefresher.Commands;
using AwsTokenRefresher.Commands.Logic;
using AwsTokenRefresher.Core.Interfaces;
using AwsTokenRefresher.Core.Models;
using AwsTokenRefresher.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ILogger = AwsTokenRefresher.Core.Interfaces.ILogger;

namespace AwsTokenRefresher.Cli
{
    public static class ServiceConfiguration
    {
        public static ServiceProvider ConfigureServices(string region, string logPath)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logPath)
                .CreateLogger();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ILogger, SerilogLogger>(_ => new SerilogLogger(logger));
            serviceCollection.AddSingleton<IAwsAuthenticator, AwsAuthenticator>();
            serviceCollection.AddSingleton<ISystemRegion>(_ => new SystemRegion(region));
            serviceCollection.AddSingleton<ICommandResolver, CommandResolver>();

            // Register commands
            serviceCollection.AddTransient<EcrAuthCommand>();
            serviceCollection.AddTransient<NuGetAuthCommand>();

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
    }
}