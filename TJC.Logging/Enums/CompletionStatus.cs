namespace TJC.Logging.Enums;

/// <summary>
/// Indicates the state of the completed <see cref="LogTracker"/>
/// </summary>
public enum CompletionStatus
{
    /// <summary>
    /// Success
    /// </summary>
    Success,

    /// <summary>
    /// Failure
    /// </summary>
    Failure,

    /// <summary>
    /// Warning
    /// </summary>
    Warning,

    /// <summary>
    /// Started
    /// </summary>
    Started,

    /// <summary>
    /// Unknown
    /// </summary>
    Unknown
}
