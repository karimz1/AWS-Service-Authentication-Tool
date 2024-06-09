using AwsServiceAuthenticator.Core.Interfaces;

namespace AwsServiceAuthenticator.Commands;

public class CommandResolver(IServiceProvider serviceProvider) : ICommandResolver
{
    private readonly Dictionary<string, Type> _commands = CommandConfiguration.GetCommandMappings();

    public ICommand? ResolveCommand(string commandName)
    {
        if (_commands.TryGetValue(commandName, out var commandType))
            return serviceProvider.GetService(commandType) as ICommand;

        throw new ArgumentException("This argument does not exist", nameof(commandName));
    }
}