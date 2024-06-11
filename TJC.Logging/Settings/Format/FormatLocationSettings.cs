namespace TJC.Logging.Settings.Format;

public class FormatLocationSettings(
    bool include = true,
    bool includeNamespace = false,
    bool includeType = true,
    bool includeMember = true,
    bool includeLineNumber = false,
    string prefix = "[",
    string separator = ".",
    string suffix = "]")
    : IPrimaryFormatterSettings
{
    #region Predefined Configurations

    public static FormatLocationSettings Default => new();

    #endregion

    #region Constants

    private const string UnknownNamespace = "UNKNOWN_NAMESPACE";

    private const string UnknownType = "UNKNOWN_TYPE";

    #endregion

    #region Properties

    public Inclusion.Inclusion Include { get; set; } = new(include);

    public Inclusion.Inclusion IncludeNamespace { get; set; } = new(includeNamespace);

    public Inclusion.Inclusion IncludeType { get; set; } = new(includeType);

    public Inclusion.Inclusion IncludeMember { get; set; } = new(includeMember);

    public Inclusion.Inclusion IncludeLineNumber { get; set; } = new(includeLineNumber);

    public Priority.Priority Priority { get; set; } = new();

    public string Prefix { get; set; } = prefix;

    public string Separator { get; set; } = separator;

    public string Suffix { get; set; } = suffix;

    #endregion

    #region IFormattable

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