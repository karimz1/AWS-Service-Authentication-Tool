using AwsServiceAuthenticator.Commands;
using AwsServiceAuthenticator.IntegrationTests.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AwsServiceAuthenticator.IntegrationTests;

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