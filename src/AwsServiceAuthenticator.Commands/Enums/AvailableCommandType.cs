using System.ComponentModel;

namespace AwsServiceAuthenticator.Commands.Enums;

public enum AvailableCommandType
{
    [Description("Execute ECR authentication.")]
    Ecr,

    [Description("Execute NuGet authentication.")]
    NuGet,

    [Description("Invalid or unsupported command.")]
    None
}