using System.Diagnostics;
using Amazon;
using Amazon.CodeArtifact;
using Amazon.CodeArtifact.Model;
using AwsServiceAuthenticator.Core.Interfaces;

namespace AwsServiceAuthenticator.Commands;

public class NuGetAuthCommand : ICommand
{
    private readonly IAwsAuthenticator _awsAuthenticator;
    private readonly ILogger _logger;
    private readonly string _region;

    public NuGetAuthCommand(IAwsAuthenticator awsAuthenticator, ILogger logger, ISystemRegion region)
    {
        _awsAuthenticator = awsAuthenticator;
        _logger = logger;
        _region = region.Region;
    }

    public async Task ExecuteAsync()
    {
        try
        {
            string domainOwner = await _awsAuthenticator.GetDomainOwnerIdAsync();
            string domainName = await _awsAuthenticator.GetDomainNameAsync();
            string token = await _awsAuthenticator.GetAuthorizationTokenAsync(domainName, domainOwner);

            var client = new AmazonCodeArtifactClient(RegionEndpoint.GetBySystemName(_region));
            var reposResponse = await client.ListRepositoriesInDomainAsync(new ListRepositoriesInDomainRequest
            {
                Domain = domainName,
                DomainOwner = domainOwner
            });

            foreach (var repo in reposResponse.Repositories)
            {
                string repoName = repo.Name;
                await RemoveNuGetSource(repoName);
                await AddNuGetSource(domainName, domainOwner, repoName, token);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Error: {ex.Message}");
        }
    }

    private async Task AddNuGetSource(string domainName, string domainOwner, string repoName, string token)
    {
        string nugetSource = $"https://{domainName}-{domainOwner}.d.codeartifact.{_region}.amazonaws.com/nuget/{repoName}/v3/index.json";

        var addSourceInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"nuget add source --name {repoName} --username aws --password {token} {nugetSource}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        var process = Process.Start(addSourceInfo);
        await process.WaitForExitAsync();

        if (process.ExitCode == 0)
        {
            _logger.LogInformation($"NuGet authentication for repository {repoName} successful.");
        }
        else
        {
            _logger.LogInformation(
                $"NuGet authentication for repository {repoName} failed: {process.StandardError.ReadToEnd()}");
        }
    }

    private async Task RemoveNuGetSource(string repoName)
    {
        var removeSourceInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"nuget remove source {repoName}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        var removeProcess = Process.Start(removeSourceInfo);
        await removeProcess.WaitForExitAsync();
        if (removeProcess.ExitCode == 0)
        {
            _logger.LogInformation($"Removed existing NuGet source: {repoName}");
        }
        else
        {
            _logger.LogError($"Failed to remove existing NuGet source: {repoName}. Error: {removeProcess.StandardError.ReadToEnd()}");
        }
    }
}