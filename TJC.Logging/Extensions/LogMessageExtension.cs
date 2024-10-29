namespace TJC.Logging.Extensions;

public static class LogMessageExtension
{
    /// <summary>
    /// Logs detailed information about the current execution location, including a message for debugging purposes.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    /// <param name="logLevel"></param>
    /// <param name="memberName">Leave blank.</param>
    /// <param name="lineNumber">Leave blank.</param>
    public static void LogMessage(
        this ILogger logger,
        string message,
        LogLevel logLevel = LogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    ) =>
        logger.LogMetadata(
            message,
            logLevel,
            frameIndex: 1,
            memberName: memberName,
            lineNumber: lineNumber
        );
}
