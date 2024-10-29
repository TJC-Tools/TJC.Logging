namespace TJC.Logging.Extensions.Specialty;

public static class LogTrackerExtensions
{
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
