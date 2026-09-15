namespace TJC.Logging.Formatter;

internal static class LogFormatter
{
    internal static Func<TState, Exception?, string> Formatter<TState>(
        string message,
        LogLevel logLevel = LogLevel.None
    ) =>
        (state, exception) =>
        {
            if (state is ILogState logState)
                return Settings.Settings.Instance.Formatting.FormatLog(
                    logState,
                    message,
                    exception,
                    logLevel
                );

            var result = state?.ToString() ?? string.Empty;
            result += message;
            if (exception != null)
                result += $" | Exception: {exception.Message}";
            return result;
        };
}
