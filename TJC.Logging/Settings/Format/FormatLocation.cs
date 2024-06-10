namespace TJC.Logging.Settings.Format;

public class FormatLocation(
    bool   include           = true,
    bool   includeNamespace  = false,
    bool   includeType       = true,
    bool   includeMember     = true,
    bool   includeLineNumber = false,
    string prefix            = "[",
    string separator         = ".",
    string suffix            = "]")
    : IFormatterSettings
{
    #region Predefined Configurations

    public static FormatLocation Default => new();

    #endregion

    #region Properties

    public Inclusion Include { get; set; } = new(include);

    public Inclusion IncludeNamespace { get; set; } = new(includeNamespace);

    public Inclusion IncludeType { get; set; } = new(includeType);

    public Inclusion IncludeMember { get; set; } = new(includeMember);

    public Inclusion IncludeLineNumber { get; set; } = new(includeLineNumber);

    public string Prefix { get; set; } = prefix;

    public string Separator { get; set; } = separator;

    public string Suffix { get; set; } = suffix;

    #endregion
}