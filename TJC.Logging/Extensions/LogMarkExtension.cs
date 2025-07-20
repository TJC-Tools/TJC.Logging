namespace TJC.Logging.Extensions;

/// <summary>
/// Extensions for logging detailed information about execution location.
/// </summary>
public static class LogMarkExtension
{
    /// <summary>
    /// Logs detailed information about the current execution location.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="memberName">Leave blank.</param>
    /// <param name="lineNumber">Leave blank.</param>
    public static void LogMark(
        this ILogger logger,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    ) =>
        logger.LogMetadata(
            logLevel: LogLevel.Trace,
            message: string.Empty,
            frameIndex: 1,
            memberName: memberName,
            lineNumber: lineNumber
        );
}
