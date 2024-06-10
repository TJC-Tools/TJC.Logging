namespace TJC.Logging.Extensions;

// ReSharper disable ExplicitCallerInfoArgument
public static class LogMarkExtension
{
    public static void LogMark(this ILogger logger,
                               [CallerMemberName] string memberName = "",
                               [CallerLineNumber] int lineNumber = 0) =>
        logger.LogMetadata(logLevel: LogLevel.Trace,
                           message: string.Empty,
                           frameIndex: 1,
                           memberName: memberName,
                           lineNumber: lineNumber);
}