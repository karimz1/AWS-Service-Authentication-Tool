using CommandLine;
using CommandLine.Text;

namespace AwsServiceAuthenticator.Cli.Utls;

public class ArgumentParserService<TOptions> where TOptions : class
{
    private readonly Parser _parser;

    public ArgumentParserService()
    {
        _parser = new Parser(settings =>
        {
            settings.HelpWriter = null;
        });
    }

    /// <summary>
    /// Parses command-line arguments into the specified options type.
    /// </summary>
    public ParserResult<TOptions> ParseArguments(string[] args)
    {
        return _parser.ParseArguments<TOptions>(args);
    }

    /// <summary>
    /// Generates help text based on the parser result.
    /// </summary>
    public string GenerateHelpText(ParserResult<TOptions> parserResult, string heading, string? preOptionsLine = null)
    {
        return HelpText.AutoBuild(parserResult, h =>
        {
            h.AdditionalNewLineAfterOption = true;
            h.Heading = heading;
            if (!string.IsNullOrEmpty(preOptionsLine))
                h.AddPreOptionsLine(preOptionsLine);
            return h;
        }, e => e);
    }
}