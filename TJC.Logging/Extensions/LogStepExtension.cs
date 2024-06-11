namespace TJC.Logging.Extensions;

public static class LogStepExtension
{
    /// <summary>
    /// Logs detailed information about the current execution location, including an auto-incrementing step number for debugging purposes.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="step">Step number</param>
    public static void LogStep(this ILogger logger, ref int step) =>
        logger.LogMetadata($"Step {++step}",
                           logLevel: LogLevel.Debug,
                           frameIndex: 1);
    /// <summary>
    /// Logs detailed information about the current execution location, including a step number for debugging purposes.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="step">Step number</param>
    public static void LogStep(this ILogger logger, int step) =>
        logger.LogMetadata($"Step {step}",
                           logLevel: LogLevel.Debug,
                           frameIndex: 1);
}