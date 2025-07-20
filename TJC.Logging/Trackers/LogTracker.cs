namespace TJC.Logging.Trackers;

/// <summary>
/// Log tracker.
/// </summary>
public class LogTracker : IFormatProvider
{
    #region Static

    internal static List<LogTracker> Trackers = [];

    #endregion

    #region Fields

    private readonly Stopwatch _stopwatch;

    #endregion

    #region Constructor

    /// <summary>
    /// Log tracker.
    /// </summary>
    public LogTracker()
    {
        Trackers.Add(this);
        StartTime = DateTime.Now;
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
        CompletionStatus = CompletionStatus.Started;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Start time of the log.
    /// </summary>
    public DateTime StartTime { get; }

    /// <summary>
    /// End time of the log.
    /// </summary>
    public DateTime? EndTime { get; private set; } = null;

    /// <summary>
    /// Status of the logs completion.
    /// </summary>
    public CompletionStatus CompletionStatus { get; private set; }

    #endregion

    #region Methods

    internal void Complete(CompletionStatus completionStatus)
    {
        _stopwatch.Stop();
        CompletionStatus = completionStatus;
        EndTime = StartTime + _stopwatch.Elapsed;
        Trackers.Remove(this);
    }

    /// <summary>
    /// Get duration of the log.
    /// </summary>
    /// <returns></returns>
    public TimeSpan? GetDuration() => _stopwatch.Elapsed;

    /// <summary>
    /// Format the log.
    /// </summary>
    /// <param name="formatType"></param>
    /// <returns></returns>
    public object? GetFormat(Type? formatType) => this;

    #region Static

    /// <summary>
    /// Get count of how many <seealso cref="LogTracker"/>'s are active.
    /// </summary>
    /// <returns></returns>
    public static int GetActiveTrackerCount() => Trackers.Count;

    #endregion

    #endregion
}
