using Serilog;
using ILogger = AwsTokenRefresher.Core.Interfaces.ILogger;

namespace AwsTokenRefresher.Infrastructure.Services;

public class SerilogLogger : ILogger
{
    private readonly Serilog.ILogger _logger;

    public SerilogLogger(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message)
    {
        _logger.Information(message);
    }

    public void LogError(string message)
    {
        _logger.Error(message);
    }

    public void LogError(string message, Exception exception)
    {
        _logger.Error(exception, message);
    }
}