namespace TJC.Logging.Settings.Format;

/// <summary>
/// Formatting settings for timestamps.
/// </summary>
/// <param name="include"></param>
public class FormatTimestampSettings(bool include = true) : IPrimaryFormatterSettings
{
    /// <summary>
    /// Include?
    /// </summary>
    public Inclusion.Inclusion Include { get; set; } = new(include);

    /// <summary>
    /// Priority.
    /// </summary>
    public Priority.Priority Priority { get; set; } = new();

    /// <summary>
    /// Timestamp format.
    /// </summary>
    public string Format { get; set; } = "yyyy-MM-ddTHH:mm:ss.fff";

    /// <summary>
    /// Format timestamp as string.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (formatProvider is not ILogState state)
            throw new NotImplementedException();
        return state.DateTime.ToString(Format);
    }
}
