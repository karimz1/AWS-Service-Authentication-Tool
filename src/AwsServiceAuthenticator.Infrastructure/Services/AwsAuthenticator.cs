using Amazon;
using Amazon.CodeArtifact;
using Amazon.CodeArtifact.Model;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using AwsServiceAuthenticator.Core.Interfaces;

namespace AwsServiceAuthenticator.Infrastructure.Services;

public class AwsAuthenticator(ILogger logger, ISystemRegion systemName) : IAwsAuthenticator
{
    private readonly string _regionSystemName = systemName.Region;

    public async Task<string> GetDomainOwnerIdAsync()
    {
        var config = new AmazonSecurityTokenServiceConfig
        {
            Timeout = TimeSpan.FromSeconds(30),
            RegionEndpoint = RegionEndpoint.GetBySystemName(_regionSystemName)
        };
        
        using var client = new AmazonSecurityTokenServiceClient(config);

        logger.LogInformation("Attempting to get caller identity...");
        var response = await client.GetCallerIdentityAsync(new GetCallerIdentityRequest());
        logger.LogInformation($"Caller identity retrieved: {response.Account}");
        return response.Account;
    }

    public async Task<string> GetDomainNameAsync()
    {
        using var client = new AmazonCodeArtifactClient(RegionEndpoint.GetBySystemName(_regionSystemName));
        logger.LogInformation("Attempting to list CodeArtifact domains...");
        var response = await client.ListDomainsAsync(new ListDomainsRequest());
        
        if (response.Domains.Count <= 0) throw new Exception("No CodeArtifact domains found.");
        
        logger.LogInformation($"Domain found: {response.Domains[0].Name}");
        return response.Domains[0].Name;

    }

    public async Task<string> GetAuthorizationTokenAsync(string domainName, string domainOwner)
    {
        using var client = new AmazonCodeArtifactClient(RegionEndpoint.GetBySystemName(_regionSystemName));
        logger.LogInformation(
            $"Attempting to get authorization token for domain: {domainName}, owner: {domainOwner}...");
        var response = await client.GetAuthorizationTokenAsync(new GetAuthorizationTokenRequest
        {
            Domain = domainName,
            DomainOwner = domainOwner
        });
        logger.LogInformation("Authorization token retrieved.");
        return response.AuthorizationToken;
    }
}