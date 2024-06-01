namespace AwsTokenRefresher.Core.Interfaces;

public interface IAwsAuthenticator
{
    Task<string> GetDomainOwnerIdAsync();
    Task<string> GetDomainNameAsync();
    Task<string> GetAuthorizationTokenAsync(string domainName, string domainOwner);
}