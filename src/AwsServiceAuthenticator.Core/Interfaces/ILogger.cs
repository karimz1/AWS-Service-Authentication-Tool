namespace AwsTokenRefresher.Core.Interfaces;

public interface ILogger
{
    void LogInformation(string message);
    void LogError(string message);
    void LogError(string message, Exception exception);
}