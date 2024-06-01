using AwsTokenRefresher.Core.Interfaces;

namespace AwsTokenRefresher.Core.Models;

public record SystemRegion : ISystemRegion
{
    public string Region { get; init; }
    
    public SystemRegion(string region)
    {
        Region = region;
    }
}