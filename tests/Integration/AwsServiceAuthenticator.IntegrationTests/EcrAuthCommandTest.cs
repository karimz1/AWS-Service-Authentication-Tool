using AwsTokenRefresher.Commands;
using AwsTokenRefresher.Tests.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AwsTokenRefresher.Tests;

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