using AwsServiceAuthenticator.Commands;
using AwsServiceAuthenticator.Commands.Handler;
using AwsServiceAuthenticator.Core.Interfaces;
using AwsServiceAuthenticator.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AwsServiceAuthenticator.IntegrationTests.Base;

public abstract class IntegrationTestBase
{
    internal static ServiceProvider ServiceProvider { get; private set; }

    [SetUp]
    public void SetUp()
    {
        var serviceCollection = new ServiceCollection();
        var loggerMock = new Mock<ILogger>();
        var systemRegionMock = new Mock<ISystemRegion>();
        systemRegionMock.Setup(s => s.Region).Returns("us-east-1");

        serviceCollection.AddSingleton(loggerMock.Object);
        serviceCollection.AddSingleton(systemRegionMock.Object);
        serviceCollection.AddSingleton<IAwsAuthenticator, AwsAuthenticator>();

        serviceCollection.AddTransient<NuGetAuthCommandHandler>();
        serviceCollection.AddTransient<EcrAuthCommandHandler>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown() => ServiceProvider?.Dispose();
}