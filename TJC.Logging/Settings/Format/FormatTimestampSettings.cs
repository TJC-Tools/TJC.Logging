namespace TJC.Logging.Settings.Format;

public class FormatTimestampSettings : IFormatterSettings
{
    public Inclusion.Inclusion Include { get; set; } = new();

    public Priority.Priority Priority { get; set; } = new();

    public string Format { get; set; } = "yyyy-MM-ddTHH:mm:ss";

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (formatProvider is not ILogState state)
            throw new NotImplementedException();
        return state.DateTime.ToString(Format);
    }
}