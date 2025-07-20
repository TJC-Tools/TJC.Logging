namespace TJC.Logging.Extensions.Specialty;

/// <summary>
/// Extensions for <seealso cref="LogTracker"/>.
/// </summary>
public static class LogTrackerExtensions
{
    /// <summary>
    /// Start a log.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="memberName"></param>
    /// <param name="lineNumber"></param>
    /// <returns></returns>
    public static LogTracker LogStart(
        this ILogger logger,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    )
    {
        var tracker = new LogTracker();
        logger.LogMetadata(
            logLevel: LogLevel.Trace,
            specialtyLogType: SpecialtyLogTypes.Tracker,
            message: Settings.Settings.Instance.Formatting.Specialty.Tracker.ToString(
                null,
                tracker
            ),
            frameIndex: 1,
            memberName: memberName,
            lineNumber: lineNumber
        );
        return tracker;
    }

    /// <summary>
    /// End a log.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="tracker"></param>
    /// <param name="completionStatus"></param>
    /// <param name="exception"></param>
    /// <param name="memberName"></param>
    /// <param name="lineNumber"></param>
    public static void LogEnd(
        this ILogger logger,
        LogTracker? tracker = null,
        CompletionStatus completionStatus = CompletionStatus.Success,
        Exception? exception = null,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    ) =>
        logger.LogEndInternal(
            tracker: tracker,
            completionStatus: completionStatus,
            exception: exception,
            frameIndex: 1,
            memberName: memberName,
            lineNumber: lineNumber
        );

    /// <summary>
    /// Logged item finished successfully.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="tracker"></param>
    /// <param name="memberName"></param>
    /// <param name="lineNumber"></param>
    public static void LogSuccess(
        this ILogger logger,
        LogTracker? tracker = null,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    ) =>
        logger.LogEndInternal(
            tracker: tracker,
            completionStatus: CompletionStatus.Success,
            exception: null,
            frameIndex: 1,
            memberName: memberName,
            lineNumber: lineNumber
        );

    /// <summary>
    /// Logged item finished with errors.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="tracker"></param>
    /// <param name="exception"></param>
    /// <param name="memberName"></param>
    /// <param name="lineNumber"></param>
    public static void LogFail(
        this ILogger logger,
        LogTracker? tracker = null,
        Exception? exception = null,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0
    ) =>
        logger.LogEndInternal(
            tracker: tracker,
            completionStatus: CompletionStatus.Failure,
            exception: exception,
            frameIndex: 1,
            memberName: memberName,
            lineNumber: lineNumber
        );

    private static void LogEndInternal(
        this ILogger logger,
        LogTracker? tracker = null,
        CompletionStatus completionStatus = CompletionStatus.Success,
        Exception? exception = null,
        int frameIndex = 0,
        string memberName = "",
        int lineNumber = 0
    )
    {
        if (completionStatus == CompletionStatus.Started)
            throw new ArgumentException(
                $"{nameof(CompletionStatus)} cannot be [{completionStatus}]",
                nameof(completionStatus)
            );

        tracker ??= new LogTracker();
        tracker.Complete(completionStatus);
        logger.LogMetadata(
            logLevel: LogLevel.Trace,
            specialtyLogType: SpecialtyLogTypes.Tracker,
            message: Settings.Settings.Instance.Formatting.Specialty.Tracker.ToString(
                null,
                tracker
            ),
            frameIndex: frameIndex + 1,
            exception: exception,
            memberName: memberName,
            lineNumber: lineNumber
        );
    }
}
