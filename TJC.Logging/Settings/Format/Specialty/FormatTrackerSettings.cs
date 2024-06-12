namespace TJC.Logging.Settings.Format.Specialty;

public class FormatTrackerSettings(
    int significantFigures = 2,
    string prefix = "[",
    string suffix = "]",
    string separator = " ",
    string started = "Started",
    string completed = "Completed",
    bool useLongUnits = false,
    bool nanoSecondsNoDecimals = true)
    : IFormattable
{
    #region Properties

    public int SignificantFigures { get; set; } = significantFigures;

    public string Separator { get; set; } = separator;

    public string Prefix { get; set; } = prefix;

    public string Suffix { get; set; } = suffix;

    public string Started { get; set; } = started;

    public string Completed { get; set; } = completed;

    public bool UseLongUnits { get; set; } = useLongUnits;

    public bool NanoSecondsNoDecimals { get; set; } = nanoSecondsNoDecimals;

    #endregion

    #region IFormattable

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (formatProvider is not LogTracker tracker)
            throw new NotImplementedException();

        var result = string.Concat(Separator, tracker.CompletionStatus);
        var duration = tracker.GetDuration();
        if (tracker.CompletionStatus == CompletionStatus.Started || duration == null)
            return result;
        var durationString = duration?.GetElapsedTime(SignificantFigures, UseLongUnits, NanoSecondsNoDecimals);
        return string.Concat(result, Separator, Prefix, durationString, Suffix);
    }

    #endregion
}