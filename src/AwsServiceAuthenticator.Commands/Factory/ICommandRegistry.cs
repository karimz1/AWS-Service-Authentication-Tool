using AwsServiceAuthenticator.Commands.Enums;
using AwsServiceAuthenticator.Commands.Handler;

namespace AwsServiceAuthenticator.Commands.Factory;

public interface ICommandRegistry
{
    /// <summary>
    /// Registers a command handler for the specified command type.
    /// </summary>
    /// <typeparam name="THandler">The type of the command handler.</typeparam>
    void MapHandlerToAvailableCommandTypeFor<THandler>(AvailableCommandType commandType) where THandler : class, ICommandHandler;

    /// <summary>
    /// Gets the instance of the handler for the given command type.
    /// </summary>
    ICommandHandler? GetCommandHandler(AvailableCommandType commandType);
}

