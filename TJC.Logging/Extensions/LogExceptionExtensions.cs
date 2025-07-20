namespace TJC.Logging.Extensions;

/// <summary>
/// Extensions for logging exceptions.
/// </summary>
public static class LogExceptionExtensions
{
    /// <summary>
    /// Logs detailed information about the current execution location, including an exception for debugging purposes.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="exception"></param>
    /// <param name="logLevel"></param>
    /// <param name="memberName">Leave blank.</param>
    /// <param name="lineNumber">Leave blank.</param>
    public static void LogException(
        this ILogger logger,
        Exception exception,
        LogLevel logLevel = LogLevel.Error,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        // Create message
        var log = new List<string>
        {
            $"Type: {exception.GetType().FullName}",
            $"Message: {exception.Message}",
            $"Source: {exception.Source}",
            $"Stack Trace: {FormatStackTrace(exception)}",
        };
        if (exception.InnerException is not null)
            log.Add($"Inner Exception: {exception.InnerException}");
        var separator = $"{Environment.NewLine}\t";
        var message = $"{separator}{string.Join(separator, log)}";

        // Log the message with metadata
        logger.LogMetadata(
            message,
            logLevel,
            frameIndex: 1,
            memberName: memberName,
            lineNumber: lineNumber
        );
    }

    private static string FormatStackTrace(Exception exception)
    {
        // Get the stack trace and trim it
        var stackTrace = exception.StackTrace ?? string.Empty;
        stackTrace = stackTrace.Trim();

        // Replace all new lines with the new line and two tabs (since the whole message is indented)
        var stackTraceNewLine = $"{Environment.NewLine}\t\t";
        stackTrace = stackTrace.Replace($"{Environment.NewLine}   ", stackTraceNewLine);

        // If the stack trace contains multiple lines, add a new line at the beginning
        if (stackTrace.Contains(Environment.NewLine))
            stackTrace = stackTrace.Insert(0, stackTraceNewLine);

        // Return the formatted stack trace
        return stackTrace;
    }
}
