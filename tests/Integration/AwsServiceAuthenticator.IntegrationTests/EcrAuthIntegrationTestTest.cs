using AwsServiceAuthenticator.Commands;
using AwsServiceAuthenticator.Commands.Handler;
using AwsServiceAuthenticator.IntegrationTests.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AwsServiceAuthenticator.IntegrationTests;

// ToDo: I'll add more tests in future, I don't have time yet!
// Currently I used Mocks but real integration tests are planned!
[TestFixture]
public class EcrAuthIntegrationTestTest : IntegrationTestBase
{
    [Test]
    public void EcrAuthCommand_ShouldNotThrow()
    {
        var command = ServiceProvider.GetRequiredService<EcrAuthCommandHandler>();
        Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync());
    }
}