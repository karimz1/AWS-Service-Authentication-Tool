namespace AwsTokenRefresher.Core.Interfaces;

public interface ICommandResolver
{
    ICommand? ResolveCommand(string commandName);
}