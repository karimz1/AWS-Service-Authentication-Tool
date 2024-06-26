﻿using System.Diagnostics;
using Amazon;
using Amazon.CodeArtifact;
using Amazon.CodeArtifact.Model;
using AwsServiceAuthenticator.Core.Interfaces;

namespace AwsServiceAuthenticator.Commands;

public class NuGetAuthCommand(IAwsAuthenticator awsAuthenticator, ILogger logger, ISystemRegion region) : ICommand
{
    private readonly string _region = region.Region;

    public async Task ExecuteAsync()
    {
            var domainOwner = await awsAuthenticator.GetDomainOwnerIdAsync();
            var domainName = await awsAuthenticator.GetDomainNameAsync();
            var token = await awsAuthenticator.GetAuthorizationTokenAsync(domainName, domainOwner);

            using var client = new AmazonCodeArtifactClient(RegionEndpoint.GetBySystemName(_region));
            var reposResponse = await client.ListRepositoriesInDomainAsync(new ListRepositoriesInDomainRequest
            {
                Domain = domainName,
                DomainOwner = domainOwner
            });

            foreach (var repoName in reposResponse.Repositories.Select(repo => repo.Name))
            {
                await RemoveNuGetSource(repoName);
                await AddNuGetSource(domainName, domainOwner, repoName, token);
            }
    }

    private async Task AddNuGetSource(string domainName, string domainOwner, string repoName, string token)
    {
        var nugetSource = $"https://{domainName}-{domainOwner}.d.codeartifact.{_region}.amazonaws.com/nuget/{repoName}/v3/index.json";

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

        logger.LogInformation(process.ExitCode == 0
            ? $"NuGet authentication for repository {repoName} successful."
            : $"NuGet authentication for repository {repoName} failed: {await process.StandardError.ReadToEndAsync()}");
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
            logger.LogInformation($"Removed existing NuGet source: {repoName}");
        else
            logger.LogError($"Failed to remove existing NuGet source: {repoName}. Error: {await removeProcess.StandardError.ReadToEndAsync()}");
    }
}