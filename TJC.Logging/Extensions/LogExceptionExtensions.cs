namespace TJC.Logging.Extensions;

public static class LogExceptionExtensions
{
    /// <summary>
    /// Logs detailed information about the current execution location, including an exception for debugging purposes.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="exception"></param>
    /// <param name="logLevel"></param>
    public static void LogException(this ILogger logger, Exception exception, LogLevel logLevel = LogLevel.Error) =>
        logger.LogMetadata(exception.ToString(),
                           logLevel,
                           frameIndex: 1);
}