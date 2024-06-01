using AwsTokenRefresher.Commands;
using AwsTokenRefresher.Tests.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AwsTokenRefresher.Tests;

[TestFixture]
public class NugetAuthCommandTest : IntegrationTestBase
{
    [Test]
    public void NuGetAuthCommand_ShouldNotThrow()
    {
        var command = ServiceProvider.GetRequiredService<NuGetAuthCommand>();
        Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync());
    }
}