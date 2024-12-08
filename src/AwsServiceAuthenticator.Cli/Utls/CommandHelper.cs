using AwsServiceAuthenticator.Commands.Enums;
using AwsServiceAuthenticator.Core.Utls;

namespace AwsServiceAuthenticator.Cli.Utls;

public static class CommandHelper
{
    public static string GetAvailableCommands()
    {
        return EnumExtensions.GetAvailableValuesWithDescriptions<AvailableCommandType>();
    }
}
