namespace TJC.Logging.Helpers;

internal static class FormatterHelpers
{
    #region Formatter Settings

    public static List<IFormatterSettings> GetFormatters(this Formatting instance)
    {
        var formatters = new List<IFormatterSettings>();
        var properties = instance.GetType().GetProperties();
        foreach (var property in properties)
            if (property.GetValue(instance) is IFormatterSettings formatter)
                formatters.Add(formatter);
        return formatters;
    }

    #endregion

    #region Inclusions

    public static void ExcludeAll(this IFormatterSettings instance)
    {
        var inclusions = instance.GetInclusions();
        foreach (var inclusion in inclusions)
        {
            var exclude = inclusion.PropertyType.GetMethod(nameof(Inclusion.Exclude));
            exclude?.Invoke(inclusion.GetValue(instance), null);
        }
    }

    public static void IncludeAll(this IFormatterSettings instance)
    {
        var inclusions = instance.GetInclusions();
        foreach (var inclusion in inclusions)
        {
            var include = inclusion.PropertyType.GetMethod(nameof(Inclusion.Include));
            include?.Invoke(inclusion.GetValue(instance), null);
        }
    }

    private static List<PropertyInfo> GetInclusions(this IFormatterSettings instance)
    {
        var properties = instance.GetType().GetProperties();
        return properties.Where(x => x.PropertyType == typeof(Inclusion)).ToList();
    }

    #endregion
}