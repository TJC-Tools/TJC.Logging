namespace TJC.Logging.Extensions;

// ReSharper disable ExplicitCallerInfoArgument
internal static class LogMetadataExtension
{
    internal static void LogMetadata(this ILogger logger,
                                   string message = "",
                                   LogLevel logLevel = LogLevel.Trace,
                                   int frameIndex = 0,
                                   [CallerMemberName] string memberName = "",
                                   [CallerLineNumber] int lineNumber = 0) =>
        logger.Log(logLevel: logLevel,
                   eventId: new EventId(0),
                   state: new LogState(frameIndex + 1, memberName, lineNumber),
                   exception: null,
                   formatter: LogFormatter.Formatter<LogState>(message));
}