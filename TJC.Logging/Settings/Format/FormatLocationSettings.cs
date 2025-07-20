namespace TJC.Logging.Settings.Format;

/// <summary>
/// Format location settings.
/// </summary>
/// <param name="include"></param>
/// <param name="includeNamespace"></param>
/// <param name="includeType"></param>
/// <param name="includeMember"></param>
/// <param name="includeLineNumber"></param>
/// <param name="prefix"></param>
/// <param name="separator"></param>
/// <param name="suffix"></param>
public class FormatLocationSettings(
    bool include = true,
    bool includeNamespace = false,
    bool includeType = true,
    bool includeMember = true,
    bool includeLineNumber = false,
    string prefix = "[",
    string separator = ".",
    string suffix = "]"
) : IPrimaryFormatterSettings
{
    #region Predefined Configurations

    /// <summary>
    /// Default settings.
    /// </summary>
    public static FormatLocationSettings Default => new();

    #endregion

    #region Constants

    private const string UnknownNamespace = "UNKNOWN_NAMESPACE";

    private const string UnknownType = "UNKNOWN_TYPE";

    #endregion

    #region Properties

    /// <summary>
    /// Include?
    /// </summary>
    public Inclusion.Inclusion Include { get; set; } = new(include);

    /// <summary>
    /// Include namespace in log message?
    /// </summary>
    public Inclusion.Inclusion IncludeNamespace { get; set; } = new(includeNamespace);

    /// <summary>
    /// Include type in log message?
    /// </summary>
    public Inclusion.Inclusion IncludeType { get; set; } = new(includeType);

    /// <summary>
    /// Include member in log message?
    /// </summary>
    public Inclusion.Inclusion IncludeMember { get; set; } = new(includeMember);

    /// <summary>
    /// Include line number in log message?
    /// </summary>
    public Inclusion.Inclusion IncludeLineNumber { get; set; } = new(includeLineNumber);

    /// <summary>
    /// Priority.
    /// </summary>
    public Priority.Priority Priority { get; set; } = new();

    /// <summary>
    /// Prefix before sections.
    /// </summary>
    public string Prefix { get; set; } = prefix;

    /// <summary>
    /// Separator between sections.
    /// </summary>
    public string Separator { get; set; } = separator;

    /// <summary>
    /// Suffix after sections.
    /// </summary>
    public string Suffix { get; set; } = suffix;

    #endregion

    #region IFormattable

    /// <summary>
    /// Format location as string.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (formatProvider is not ILogState state)
            throw new NotImplementedException();

        var sections = new List<string>();

        if (IncludeNamespace)
            sections.Add(state.CallingType?.Namespace ?? UnknownNamespace);

        if (IncludeType)
            sections.Add(state.CallingType?.Name ?? UnknownType);

        if (IncludeMember)
            sections.Add(state.MemberName);

        if (IncludeLineNumber)
            sections.Add(state.LineNumber.ToString());

        return string.Concat(Prefix, string.Join(Separator, sections), Suffix);
    }

    #endregion
}
