using AwsServiceAuthenticator.Core.Interfaces;

namespace AwsServiceAuthenticator.Commands.Logic;

public class CommandResolver : ICommandResolver
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, Type> _commands;
    
    public CommandResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _commands = new Dictionary<string, Type>
        {
            { "ecr", typeof(EcrAuthCommand) },
            { "nuget", typeof(NuGetAuthCommand) }
        };
    }

    ICommand? ICommandResolver.ResolveCommand(string commandName)
    {
        if (_commands.TryGetValue(commandName, out var commandType))
        {
            return _serviceProvider.GetService(commandType) as ICommand;
        }

        throw new ArgumentException("This argument does not exists", nameof(commandName));
    }
}