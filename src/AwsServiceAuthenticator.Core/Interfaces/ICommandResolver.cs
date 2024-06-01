namespace AwsServiceAuthenticator.Core.Interfaces;

public interface ICommandResolver
{
    ICommand? ResolveCommand(string commandName);
}