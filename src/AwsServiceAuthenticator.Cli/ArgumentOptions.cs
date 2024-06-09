using CommandLine;

namespace AwsServiceAuthenticator.Cli;

internal class ArgumentOptions
{
    [Option('c', "command", Required = true, HelpText = "Command to execute (ecr or nuget).")]
    public string Command { get; set; }

    [Option('r', "region", Required = true, HelpText = "AWS region.")]
    public string Region { get; set; }

    [Option('l', "logFolderPath", Required = true, HelpText = "Path to log folder.")]
    public string LogFolderPath { get; set; }
}
