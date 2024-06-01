namespace AwsTokenRefresher.Core.Interfaces;

public interface ICommand
{
    Task ExecuteAsync();
}