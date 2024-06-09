namespace TJC.Logging.Extensions;

public static class LogStepExtension
{
    public static void LogStep(this ILogger logger, int step) =>
        logger.LogMetadata($"Step {step}",
                           logLevel: LogLevel.Debug,
                           frameIndex: 1);
}