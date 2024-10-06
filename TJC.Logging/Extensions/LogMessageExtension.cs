namespace TJC.Logging.Extensions;

public static class LogMessageExtension
{
    /// <summary>
    /// Logs detailed information about the current execution location, including a message for debugging purposes.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    /// <param name="logLevel"></param>
    public static void LogMessage(this ILogger logger, string message, LogLevel logLevel = LogLevel.Debug) =>
        logger.LogMetadata(message,
                           logLevel,
                           frameIndex: 1);
}