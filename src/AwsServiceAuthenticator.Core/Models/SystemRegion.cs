using AwsServiceAuthenticator.Core.Interfaces;

namespace AwsServiceAuthenticator.Core.Models;

public record SystemRegion : ISystemRegion
{
    public string Region { get; }
    
    public SystemRegion(string region)
    {
        Region = region;
    }
}