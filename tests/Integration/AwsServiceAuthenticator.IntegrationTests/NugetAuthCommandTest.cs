using AwsServiceAuthenticator.Commands;
using AwsServiceAuthenticator.Commands.Handler;
using AwsServiceAuthenticator.IntegrationTests.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AwsServiceAuthenticator.IntegrationTests;

// ToDo: I'll add more tests in future, I don't have time yet!
// Currently I used Mocks but real integration tests are planned!
[TestFixture]
public class NugetAuthCommandTest : IntegrationTestBase
{
    [Test]
    public void NuGetAuthCommand_ShouldNotThrow()
    {
        var command = ServiceProvider.GetRequiredService<NuGetAuthCommandHandler>();
        Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync());
    }
}