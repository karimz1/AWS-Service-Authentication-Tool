using AwsServiceAuthenticator.Commands.Enums;
using AwsServiceAuthenticator.Commands.Handler;

namespace AwsServiceAuthenticator.Commands.Factory;

public class CommandRegistry(IServiceProvider serviceProvider) : ICommandRegistry
{
    private readonly Dictionary<AvailableCommandType, Type> _commandMappings = new();

    public void MapHandlerToAvailableCommandTypeFor<THandler>(AvailableCommandType commandType)
        where THandler : class, ICommandHandler
    {
        _commandMappings[commandType] = typeof(THandler);
    }

    public ICommandHandler? GetCommandHandler(AvailableCommandType commandType)
    {
        if (_commandMappings.TryGetValue(commandType, out var handlerType))
        {
            return serviceProvider.GetService(handlerType) as ICommandHandler;
        }

        return null;
    }
}



