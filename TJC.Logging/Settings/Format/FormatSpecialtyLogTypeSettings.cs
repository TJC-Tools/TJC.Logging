namespace TJC.Logging.Settings.Format;

/// <summary>
/// Formatting settings for specialty log types.
/// </summary>
/// <param name="include"></param>
/// <param name="prefix"></param>
/// <param name="suffix"></param>
public class FormatSpecialtyLogTypeSettings(
    bool include = true,
    string prefix = "[",
    string suffix = "]"
) : IPrimaryFormatterSettings
{
    #region Properties

    /// <summary>
    /// Include?
    /// </summary>
    public Inclusion.Inclusion Include { get; set; } = new(include);

    /// <summary>
    /// Priority.
    /// </summary>
    public Priority.Priority Priority { get; set; } = new();

    /// <summary>
    /// Prefix before specialty.
    /// </summary>
    public string Prefix { get; set; } = prefix;

    /// <summary>
    /// Suffix after specialty.
    /// </summary>
    public string Suffix { get; set; } = suffix;

    /// <summary>
    /// Formatting settings for <seealso cref="LogGetExtension"/> &amp; <seealso cref="LogSetExtension"/>.
    /// </summary>
    public FormatGetSetSettings GetSet { get; set; } = new();

    /// <summary>
    /// Formatting settings for <seealso cref="LogTracker"/>.
    /// </summary>
    public FormatTrackerSettings Tracker { get; set; } = new();

    #endregion

    #region IFormattable

    /// <summary>
    /// Format specialty as string.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
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
