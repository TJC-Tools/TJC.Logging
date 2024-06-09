namespace TJC.Logging.Extensions;

public static class MarkExtension
{
    // ReSharper disable ExplicitCallerInfoArgument
    public static void Mark(this               ILogger logger,
                            [CallerMemberName] string  memberName = "",
                            [CallerLineNumber] int     lineNumber = 0) =>
        logger.LogMetadata(logLevel: LogLevel.Trace,
                           message: string.Empty,
                           frameIndex: 1,
                           memberName: memberName,
                           lineNumber: lineNumber);
}