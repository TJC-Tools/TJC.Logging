namespace TJC.Logging.Helpers;

internal static class FormatterHelpers
{
    public static List<IPrimaryFormatterSettings> GetFormatters(this FormattingSettings instance,
                                                                bool includedOnly = false,
                                                                bool prioritized = false)
    {
        var formatters = instance.GetAllFormatters();
        if (includedOnly)
            formatters = [.. formatters.Where(x => x.Include)];
        if (prioritized)
            formatters = [.. formatters.OrderBy(x => x.Priority)];
        return formatters;
    }

    public static List<IPrimaryFormatterSettings> GetAllFormatters(this FormattingSettings instance)
    {
        var formatters = new List<IPrimaryFormatterSettings>();
        var properties = instance.GetType().GetProperties();
        foreach (var property in properties)
            if (property.GetValue(instance) is IPrimaryFormatterSettings formatter)
                formatters.Add(formatter);
        return formatters;
    }
}