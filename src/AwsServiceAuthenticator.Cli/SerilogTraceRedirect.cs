using System.Diagnostics;
using AwsServiceAuthenticator.Core.Interfaces;


namespace AwsServiceAuthenticator.Cli;

public class SerilogTraceRedirect(ILogger logger) : TraceListener
{
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public override void Write(string? message)
    {
        if (!string.IsNullOrEmpty(message))
            _logger.LogDebug(message);
    }

    public override void WriteLine(string? message)
    {
        if (!string.IsNullOrEmpty(message))
            _logger.LogDebug(message);
    }
}