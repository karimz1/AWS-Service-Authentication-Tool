using Serilog;
using ILogger = AwsServiceAuthenticator.Core.Interfaces.ILogger;
using Interfaces_ILogger = AwsServiceAuthenticator.Core.Interfaces.ILogger;

namespace AwsServiceAuthenticator.Infrastructure.Services;

public class SerilogLogger : Interfaces_ILogger
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