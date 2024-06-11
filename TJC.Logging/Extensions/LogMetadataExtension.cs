namespace TJC.Logging.Extensions;

// ReSharper disable ExplicitCallerInfoArgument
internal static class LogMetadataExtension
{
    /// <summary>
    /// This is the base internal method called by all other extensions to make use of the <see cref="LogState"/> &amp; <see cref="LogFormatter"/>.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    /// <param name="logLevel"></param>
    /// <param name="frameIndex"></param>
    /// <param name="memberName"></param>
    /// <param name="lineNumber"></param>
    internal static void LogMetadata(this ILogger logger,
                                     string message = "",
                                     LogLevel logLevel = LogLevel.Trace,
                                     int frameIndex = 0,
                                     SpecialtyLogTypes specialtyLogType = SpecialtyLogTypes.None,
                                     [CallerMemberName] string memberName = "",
                                     [CallerLineNumber] int lineNumber = 0) =>
        logger.Log(logLevel: logLevel,
                   eventId: new EventId(0),
                   state: new LogState(
                       frameIndex: frameIndex + 1,
                       specialtyLogType: specialtyLogType,
                       memberName: memberName,
                       lineNumber: lineNumber),
                   exception: null,
                   formatter: LogFormatter.Formatter<LogState>(message));
}