using AwsServiceAuthenticator.Commands;
using AwsServiceAuthenticator.IntegrationTests.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AwsServiceAuthenticator.IntegrationTests;

[TestFixture]
public class EcrAuthIntegrationTestTest : IntegrationTestBase
{
    [Test]
    public void EcrAuthCommand_ShouldNotThrow()
    {
        var command = ServiceProvider.GetRequiredService<EcrAuthCommand>();
        Assert.DoesNotThrowAsync(async () => await command.ExecuteAsync());
    }
}