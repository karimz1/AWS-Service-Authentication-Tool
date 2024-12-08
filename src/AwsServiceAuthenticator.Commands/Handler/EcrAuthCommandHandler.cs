using System.Diagnostics;
using Amazon;
using Amazon.ECR;
using AwsServiceAuthenticator.Core.Interfaces;

namespace AwsServiceAuthenticator.Commands.Handler;

public class EcrAuthCommandHandler(ILogger logger, ISystemRegion systemRegion) : ICommandHandler
{
    private readonly string _systemRegion = systemRegion.Region;

    public async Task ExecuteAsync()
    {
            using var client = new AmazonECRClient(RegionEndpoint.GetBySystemName(_systemRegion));
            var response = await client.GetAuthorizationTokenAsync(new Amazon.ECR.Model.GetAuthorizationTokenRequest());
            var authData = response.AuthorizationData[0];
            var token = Convert.FromBase64String(authData.AuthorizationToken);
            var credentials = System.Text.Encoding.UTF8.GetString(token).Split(':');
            var username = credentials[0];
            var password = credentials[1];

            var startInfo = new ProcessStartInfo
            {
                FileName = "docker",
                Arguments = $"login --username {username} --password {password} {authData.ProxyEndpoint}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = Process.Start(startInfo);
            await process.WaitForExitAsync();

            logger.LogInformation(process.ExitCode == 0
                ? "ECR authentication successful."
                : $"ECR authentication failed, details: {await process.StandardError.ReadToEndAsync()}");
    }
}