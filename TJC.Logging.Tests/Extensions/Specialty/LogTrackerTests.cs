namespace TJC.Logging.Tests.Extensions.Specialty;

[TestClass]
public class LogTrackerTests
{
    private readonly MockTraceLogger _logger = new();

    [TestMethod]
    public void LogStart_ShouldInitializeLogTracker()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Specialty.Include.Include();
        Settings.Settings.Instance.Formatting.Specialty.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Tracker.Separator = string.Empty;

        // Act
        var tracker = _logger.LogStart();

        // Assert
        var message = string.Concat(SpecialtyLogTypes.Tracker, CompletionStatus.Started);
        Assert.AreEqual(message, _logger.LastMessage);
        Assert.IsNotNull(tracker);
        Assert.AreEqual(CompletionStatus.Started, tracker.CompletionStatus);
        Assert.IsTrue(LogTracker.Trackers.Contains(tracker));
    }

    [TestMethod]
    public void LogEnd_ShouldCompleteLogTracker_WithSuccessStatus()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Specialty.Include.Include();
        Settings.Settings.Instance.Formatting.Specialty.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Tracker.Separator = string.Empty;
        var tracker = new LogTracker();

        // Act
        _logger.LogEnd(tracker, CompletionStatus.Success);

        // Assert
        var message = string.Concat(SpecialtyLogTypes.Tracker, CompletionStatus.Success);
        Assert.IsTrue(_logger.LastMessage?.StartsWith(message));
        Assert.AreEqual(CompletionStatus.Success, tracker.CompletionStatus);
        Assert.IsNotNull(tracker.EndTime);
        Assert.IsFalse(LogTracker.Trackers.Contains(tracker));
    }

    [TestMethod]
    public void LogEnd_ShouldCompleteLogTracker_WithFailureStatus()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Specialty.Include.Include();
        Settings.Settings.Instance.Formatting.Specialty.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Tracker.Separator = string.Empty;
        var tracker = new LogTracker();

        // Act
        _logger.LogEnd(tracker, CompletionStatus.Failure);

        // Assert
        var message = string.Concat(SpecialtyLogTypes.Tracker, CompletionStatus.Failure);
        Assert.IsTrue(_logger.LastMessage?.StartsWith(message));
        Assert.AreEqual(CompletionStatus.Failure, tracker.CompletionStatus);
        Assert.IsNotNull(tracker.EndTime);
        Assert.IsFalse(LogTracker.Trackers.Contains(tracker));
    }

    [TestMethod]
    public void LogEnd_ShouldInitializeNewTracker_IfNull()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Specialty.Include.Include();
        Settings.Settings.Instance.Formatting.Specialty.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Tracker.Separator = string.Empty;

        // Act
        _logger.LogEnd(null, CompletionStatus.Success);

        // Assert
        var message = string.Concat(SpecialtyLogTypes.Tracker, CompletionStatus.Success);
        Assert.IsTrue(_logger.LastMessage?.StartsWith(message));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void LogEnd_ShouldThrowArgumentException_ForStartedStatus()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Specialty.Include.Include();
        Settings.Settings.Instance.Formatting.Specialty.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Tracker.Separator = string.Empty;
        var tracker = new LogTracker();

        // Act
        _logger.LogEnd(tracker, CompletionStatus.Started);
    }
}