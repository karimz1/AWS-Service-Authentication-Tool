namespace AwsServiceAuthenticator.Commands;

public static class CommandConfiguration
{
    public static Dictionary<string, Type> GetCommandMappings()
    {
        return new Dictionary<string, Type>
        {
            { "ecr", typeof(EcrAuthCommand) },
            { "nuget", typeof(NuGetAuthCommand) }
        };
    }
}
