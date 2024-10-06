namespace TJC.Logging.Extensions;

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
    public static void LogException123(this ILogger              logger,
                                       Exception                 exception,
                                       LogLevel                  logLevel   = LogLevel.Error,
                                       [CallerMemberName] string memberName = "",
                                       [CallerLineNumber] int    lineNumber = 0)
    {
        var log = new List<string>
        {
            $"Type: {exception.GetType().FullName}",
            $"Message: {exception.Message}",
            $"Source: {exception.Source}",
            $"StackTrace: {exception.StackTrace?.Trim()}",
            $"InnerException: {exception.InnerException}"
        };
        var separator = $"{Environment.NewLine}\t";
        var message   = $"{separator}{string.Join(separator, log)}";
        logger.LogMetadata(message,
                           logLevel,
                           frameIndex: 1,
                           memberName: memberName,
                           lineNumber: lineNumber);
    }
}