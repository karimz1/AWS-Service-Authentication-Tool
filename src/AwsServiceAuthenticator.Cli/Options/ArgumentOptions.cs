using AwsServiceAuthenticator.Commands.Enums;
using CommandLine;

namespace AwsServiceAuthenticator.Cli.Options;
internal class ArgumentOptions
{
    [Option('c', "command", Required = true, HelpText = "Command to execute.")]
    public string CommandRaw { get; set; } = string.Empty;

    public AvailableCommandType Command => Enum.TryParse(CommandRaw, true, out AvailableCommandType result) ? result : AvailableCommandType.None;


    [Option('r', "region", Required = true, HelpText = "AWS region.")]
    public string Region { get; set; } = string.Empty;

    [Option('l', "logFolderPath", Required = true, HelpText = "Path to log folder.")]
    public string LogFolderPath { get; set; } = string.Empty;
    
    [Option('d', "debugMode", Required = false, HelpText = "Enhance Logging using DebugMode")]
    public bool DebugMode { get; set; }
}
