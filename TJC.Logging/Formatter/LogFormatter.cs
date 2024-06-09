namespace TJC.Logging.Formatter;

internal static class LogFormatter
{
    internal static Func<TState, Exception?, string> Formatter<TState>(string message) =>
        (state, exception) =>
        {
            var result = state?.ToString() ?? string.Empty;
            result += message;
            if (exception != null)
                result += $" | Exception: {exception.Message}";
            return result;
        };
}