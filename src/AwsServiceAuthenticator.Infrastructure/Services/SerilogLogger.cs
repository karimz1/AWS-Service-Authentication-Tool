using Interfaces_ILogger = AwsServiceAuthenticator.Core.Interfaces.ILogger;

namespace AwsServiceAuthenticator.Infrastructure.Services;

public class SerilogLogger(Serilog.ILogger logger) : Interfaces_ILogger
{
    public void LogInformation(string message)
    {
        logger.Information(message);
    }

    public void LogError(string message)
    {
        logger.Error(message);
    }

    public void LogError(string message, Exception exception)
    {
        logger.Error(exception, message);
    }

    public void LogDebug(string message)
    {
        logger.Debug(message);
    }
}