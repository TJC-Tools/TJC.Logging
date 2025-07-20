namespace TJC.Logging.Settings.Format;

/// <summary>
/// Formatting settings.
/// </summary>
public class FormattingSettings : IFormattable
{
    #region Constructor

    /// <summary>
    /// Formatting settings.
    /// </summary>
    public FormattingSettings()
    {
        Timestamp.Priority.Then(Specialty.Priority).Then(Location.Priority);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Separator between sections.
    /// </summary>
    public string Separator { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp formatting settings.
    /// </summary>
    public FormatTimestampSettings Timestamp { get; set; } = new();

    /// <summary>
    /// Location formatting settings.
    /// </summary>
    public FormatLocationSettings Location { get; set; } = new();

    /// <summary>
    /// Specialty formatting settings.
    /// </summary>
    public FormatSpecialtyLogTypeSettings Specialty { get; set; } = new();

    #endregion

    #region Methods

    /// <summary>
    /// Exclude all formatters.
    /// </summary>
    public void ExcludeAll()
    {
        foreach (var formatter in this.GetAllFormatters())
            formatter.ExcludeAll();
    }

    /// <summary>
    /// Include all formatters.
    /// </summary>
    public void IncludeAll()
    {
        foreach (var formatter in this.GetAllFormatters())
            formatter.IncludeAll();
    }

    #region IFormattable

    /// <summary>
    /// Format as string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => ToString(null, null);

    /// <summary>
    /// Format as string.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (formatProvider is not ILogState state)
            throw new NotImplementedException();
        var formatters = this.GetFormatters(includedOnly: true, prioritized: true);
        var sections = formatters.Select(formatter => formatter.ToString(format, state));
        return string.Join(Separator, sections);
    }

    #endregion

    #endregion
}
