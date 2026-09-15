namespace TJC.Logging.Settings.Format;

/// <summary>
/// Formats log entries as Markdown callouts.
/// </summary>
public class FormatMarkdownSettings : FormattingSettings
{
    #region Constructor

    /// <summary>
    /// Initializes Markdown callout formatting settings.
    /// </summary>
    public FormatMarkdownSettings()
    {
        Timestamp.Include = new(false);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Whether the callout is collapsed by default.
    /// </summary>
    public bool IsCollapsed { get; set; } = true;

    /// <summary>
    /// Template for the callout title. Supports <c>{Title}</c>, <c>{Timestamp}</c>,
    /// <c>{Message}</c>, and <c>{Exception}</c> placeholders.
    /// </summary>
    public string TitleFormat { get; set; } = "{Title}";

    /// <summary>
    /// Template for the callout contents. Supports <c>{Title}</c>, <c>{Timestamp}</c>,
    /// <c>{Message}</c>, and <c>{Exception}</c> placeholders.
    /// </summary>
    public string ContentFormat { get; set; } = "{Timestamp}\n{Message}\nException: {Exception}";

    #endregion

    #region Methods

    internal override string FormatLog(
        ILogState state,
        string message,
        Exception? exception,
        LogLevel logLevel
    )
    {
        var formattedTitle = ToString(null, state);
        if (string.IsNullOrWhiteSpace(formattedTitle))
            formattedTitle = message;

        var values = new Dictionary<string, string>
        {
            ["{Title}"] = formattedTitle,
            ["{Timestamp}"] = Timestamp.ToString(null, state),
            ["{Message}"] = message,
            ["{Exception}"] = exception?.Message ?? string.Empty,
        };
        var lines = new List<string>
        {
            $"[!{GetCalloutType(logLevel, exception)}]{(IsCollapsed ? "-" : string.Empty)} {FormatTemplate(TitleFormat, values)}",
        };
        lines.AddRange(FormatTemplateLines(ContentFormat, values));

        return string.Concat(
            "> ",
            string.Join($"{Environment.NewLine}> ", lines.Select(FormatLines)),
            Environment.NewLine,
            Environment.NewLine
        );
    }

    private static string GetCalloutType(LogLevel logLevel, Exception? exception) =>
        exception is not null
            ? "FAIL"
            : logLevel switch
            {
                LogLevel.Trace => "QUOTE",
                LogLevel.Debug => "QUOTE",
                LogLevel.Information => "INFO",
                LogLevel.Warning => "WARNING",
                LogLevel.Error => "FAIL",
                LogLevel.Critical => "DANGER",
                _ => "NOTE",
            };

    private static IEnumerable<string> FormatTemplateLines(
        string template,
        IReadOnlyDictionary<string, string> values
    )
    {
        var lines = template.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
        return lines
            .Where(line => !values.Any(value => line.Contains(value.Key) && string.IsNullOrEmpty(value.Value)))
            .Select(line => FormatTemplate(line, values))
            .Where(line => !string.IsNullOrEmpty(line));
    }

    private static string FormatTemplate(string template, IReadOnlyDictionary<string, string> values)
    {
        foreach (var value in values)
            template = template.Replace(value.Key, value.Value);
        return template;
    }

    private static string FormatLines(string value) =>
        value.Replace("\r\n", "\n").Replace('\r', '\n').Replace("\n", $"{Environment.NewLine}> ");

    #endregion
}