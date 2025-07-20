namespace TJC.Logging.Settings.Format.Specialty;

/// <summary>
/// Formatting settings for <seealso cref="LogTracker"/>.
/// </summary>
/// <param name="significantFigures"></param>
/// <param name="prefix"></param>
/// <param name="suffix"></param>
/// <param name="separator"></param>
/// <param name="useLongUnits"></param>
/// <param name="nanoSecondsNoDecimals"></param>
public class FormatTrackerSettings(
    int significantFigures = 2,
    string prefix = "[",
    string suffix = "]",
    string separator = " ",
    bool useLongUnits = false,
    bool nanoSecondsNoDecimals = true
) : IFormattable
{
    #region Properties

    /// <summary>
    /// Significant figures for tracking time.
    /// </summary>
    public int SignificantFigures { get; set; } = significantFigures;

    /// <summary>
    /// Separator between <seealso cref="CompletionStatus"/> &amp; <seealso cref="Prefix"/>.
    /// </summary>
    public string Separator { get; set; } = separator;

    /// <summary>
    /// Prefix before duration.
    /// </summary>
    public string Prefix { get; set; } = prefix;

    /// <summary>
    /// Suffix after duration.
    /// </summary>
    public string Suffix { get; set; } = suffix;

    /// <summary>
    /// Whether to use long or short units.
    /// </summary>
    public bool UseLongUnits { get; set; } = useLongUnits;

    /// <summary>
    /// If the result is in nanoseconds, should decimals be displayed?
    /// </summary>
    public bool NanoSecondsNoDecimals { get; set; } = nanoSecondsNoDecimals;

    #endregion

    #region IFormattable

    /// <summary>
    /// Format <seealso cref="LogTracker"/> as string.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (formatProvider is not LogTracker tracker)
            throw new NotImplementedException();

        var result = string.Concat(Separator, tracker.CompletionStatus);
        var duration = tracker.GetDuration();
        if (tracker.CompletionStatus == CompletionStatus.Started || duration == null)
            return result;
        var durationString = duration.Value.GetElapsedTime(
            SignificantFigures,
            UseLongUnits,
            NanoSecondsNoDecimals
        );
        return string.Concat(result, Separator, Prefix, durationString, Suffix);
    }

    #endregion
}
