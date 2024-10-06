namespace TJC.Logging.Trackers;

public class LogTracker : IFormatProvider
{
    #region Static

    internal static List<LogTracker> Trackers = [];

    #endregion

    #region Constructor

    public LogTracker()
    {
        Trackers.Add(this);
        StartTime = DateTime.Now;
        CompletionStatus = CompletionStatus.Started;
    }

    #endregion

    #region Properties

    public DateTime StartTime { get; }

    public DateTime? EndTime { get; private set; } = null;

    public CompletionStatus CompletionStatus { get; private set; }

    #endregion

    #region Methods

    internal void Complete(CompletionStatus completionStatus)
    {
        CompletionStatus = completionStatus;
        EndTime = DateTime.Now;
        Trackers.Remove(this);
    }

    public TimeSpan? GetDuration() =>
        EndTime.HasValue ?
            EndTime.Value - StartTime :
            null;

    public object? GetFormat(Type? formatType) =>
        this;

    #region Static  

    public static int GetActiveTrackerCount() =>
        Trackers.Count;

    #endregion

    #endregion
}