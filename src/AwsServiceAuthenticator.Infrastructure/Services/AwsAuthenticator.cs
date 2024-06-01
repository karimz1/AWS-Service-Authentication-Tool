using Amazon;
using Amazon.CodeArtifact;
using Amazon.CodeArtifact.Model;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using AwsTokenRefresher.Core.Interfaces;

namespace AwsTokenRefresher.Infrastructure.Services;

public class AwsAuthenticator : IAwsAuthenticator
{
    private readonly ILogger _logger;
    private readonly string _regionSystemName;

    public AwsAuthenticator(ILogger logger, ISystemRegion systemName)
    {
        _logger = logger;
        _regionSystemName = systemName.Region;
    }
    public async Task<string> GetDomainOwnerIdAsync()
    {
        try
        {
            var config = new AmazonSecurityTokenServiceConfig
            {
                Timeout = TimeSpan.FromSeconds(30),
                RegionEndpoint = RegionEndpoint.GetBySystemName(_regionSystemName)
            };
            var stsClient = new AmazonSecurityTokenServiceClient(config);

           _logger.LogInformation("Attempting to get caller identity...");
            var response = await stsClient.GetCallerIdentityAsync(new GetCallerIdentityRequest());
           _logger.LogInformation($"Caller identity retrieved: {response.Account}");
            return response.Account;
        }
        catch (AmazonServiceException ex)
        {
            _logger.LogInformation($"Amazon service exception: {ex.Message}");
            _logger.LogInformation($"Stack trace: {ex.StackTrace}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Exception: {ex.Message}");
            _logger.LogInformation($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<string> GetDomainNameAsync()
    {
        try
        {
            using var client = new AmazonCodeArtifactClient(RegionEndpoint.GetBySystemName(_regionSystemName));
            _logger.LogInformation("Attempting to list CodeArtifact domains...");
            var response = await client.ListDomainsAsync(new ListDomainsRequest());
            if (response.Domains.Count > 0)
            {
                _logger.LogInformation($"Domain found: {response.Domains[0].Name}");
                return response.Domains[0].Name;
            }
            throw new Exception("No CodeArtifact domains found.");
        }
        catch (AmazonServiceException ex)
        {
            _logger.LogInformation($"Amazon service exception: {ex.Message}");
            _logger.LogInformation($"Stack trace: {ex.StackTrace}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Exception: {ex.Message}");
            _logger.LogInformation($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<string> GetAuthorizationTokenAsync(string domainName, string domainOwner)
    {
        try
        {
            using var client = new AmazonCodeArtifactClient(RegionEndpoint.GetBySystemName(_regionSystemName));
            _logger.LogInformation($"Attempting to get authorization token for domain: {domainName}, owner: {domainOwner}...");
            var response = await client.GetAuthorizationTokenAsync(new GetAuthorizationTokenRequest
            {
                Domain = domainName,
                DomainOwner = domainOwner
            });
            _logger.LogInformation("Authorization token retrieved.");
            return response.AuthorizationToken;
        }
        catch (AmazonServiceException ex)
        {
            _logger.LogInformation($"Amazon service exception: {ex.Message}");
            _logger.LogInformation($"Stack trace: {ex.StackTrace}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Exception: {ex.Message}");
            _logger.LogInformation($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }
}