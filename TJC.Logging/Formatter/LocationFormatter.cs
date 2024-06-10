namespace TJC.Logging.Formatter;

internal static class LocationFormatter
{
    #region Constants

    private const string UnknownNamespace = "UNKNOWN_NAMESPACE";

    private const string UnknownType = "UNKNOWN_TYPE";

    #endregion

    #region Settings

    private static FormatLocation Settings =>
        Logging.Settings.Settings.Instance.Formatting.Location;

    private static string Separator =>
        Settings.Separator;

    #endregion

    #region

    internal static string FormatLocation(this LogState state)
    {
        var result = string.Empty;

        if (!Settings.Include)
            return result;

        if (Settings.IncludeNamespace)
            SeparatorHelpers.AddSeparator(ref result, Separator, state.CallingType?.Namespace ?? UnknownNamespace);

        if (Settings.IncludeType)
            SeparatorHelpers.AddSeparator(ref result, Separator, state.CallingType?.Name ?? UnknownType);

        if (Settings.IncludeMember)
            SeparatorHelpers.AddSeparator(ref result, Separator, state.MemberName);

        if (Settings.IncludeLineNumber)
            SeparatorHelpers.AddSeparator(ref result, Separator, state.LineNumber);

        return string.Concat(Settings.Prefix, result, Settings.Suffix);
    }

    #endregion
}