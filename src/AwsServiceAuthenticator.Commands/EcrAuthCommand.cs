using System.Diagnostics;
using Amazon;
using Amazon.ECR;
using AwsTokenRefresher.Core.Interfaces;

namespace AwsTokenRefresher.Commands;

public class EcrAuthCommand : ICommand
{
    private readonly ILogger _logger;
    private readonly string _systemRegion;

    public EcrAuthCommand(ILogger logger, ISystemRegion systemRegion)
    {
        _logger = logger;
        _systemRegion = systemRegion.Region;
    }

    public async Task ExecuteAsync()
    {
        try
        {
            var ecrClient = new AmazonECRClient(RegionEndpoint.GetBySystemName(_systemRegion));
            var response =
                await ecrClient.GetAuthorizationTokenAsync(new Amazon.ECR.Model.GetAuthorizationTokenRequest());
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

            if (process.ExitCode == 0)
            {
               _logger.LogInformation("ECR authentication successful.");
            }
            else
            {
                _logger.LogInformation($"ECR authentication failed: {process.StandardError.ReadToEnd()}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Error: {ex.Message}");
        }
    }
}