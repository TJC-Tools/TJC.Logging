namespace TJC.Logging.Settings.Format;

public class FormatSpecialtyLogTypeSettings(
    bool include = true,
    string prefix = "[",
    string suffix = "]")
    : IPrimaryFormatterSettings
{
    #region Properties

    public Inclusion.Inclusion Include { get; set; } = new(include);
    public Priority.Priority Priority { get; set; } = new();

    public string Prefix { get; set; } = prefix;

    public string Suffix { get; set; } = suffix;

    public FormatGetSetSettings GetSet { get; set; } = new();

    public FormatTrackerSettings Tracker { get; set; } = new();

    #endregion

    #region IFormattable    

    public string ToString(string? format, IFormatProvider? formatProvider)
    {

        if (formatProvider is not ILogState state)
            throw new NotImplementedException();
        if (state.Specialty == SpecialtyLogTypes.None)
            return string.Empty;
        return string.Concat(Prefix, state.Specialty, Suffix);
    }

    #endregion
}