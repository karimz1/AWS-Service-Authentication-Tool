namespace AwsServiceAuthenticator.Core.Interfaces;

public interface ICommand
{
    Task ExecuteAsync();
}