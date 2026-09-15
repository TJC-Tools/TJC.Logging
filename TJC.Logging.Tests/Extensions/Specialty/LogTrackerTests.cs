namespace TJC.Logging.Tests.Extensions.Specialty;

[TestClass]
public class LogTrackerTests
{
    private readonly MockTraceLogger _logger = new();

    [TestInitialize]
    public void Initialize() => Settings.Settings.ReloadDefaults();

    [TestMethod]
    public void LogStart_ShouldInitializeLogTracker()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Specialty.Include.Include();
        Settings.Settings.Instance.Formatting.Specialty.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Tracker.Separator = string.Empty;
        var name = "My Example Logger";

        // Act
        var tracker = _logger.LogStart(message: name);

        // Assert
        var message = string.Concat(SpecialtyLogTypes.Tracker, name, CompletionStatus.Started);
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
        var name = "My Test Tracker";
        var tracker = new LogTracker(name);

        // Act
        _logger.LogEnd(tracker, CompletionStatus.Success);

        // Assert
        var message = string.Concat(SpecialtyLogTypes.Tracker, name, CompletionStatus.Success);
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

    [TestMethod]
    public void LogSuccess_ShouldCompleteTracker()
    {
        // Arrange
        var tracker = new LogTracker();

        // Act
        _logger.LogSuccess(tracker);

        // Assert
        Assert.AreEqual(CompletionStatus.Success, tracker.CompletionStatus);
        Assert.IsNotNull(tracker.EndTime);
    }

    [TestMethod]
    public void LogFail_ShouldCompleteTrackerAndLogException()
    {
        // Arrange
        var tracker = new LogTracker();
        var exception = new InvalidOperationException("failure");

        // Act
        _logger.LogFail(tracker, exception);

        // Assert
        Assert.AreEqual(CompletionStatus.Failure, tracker.CompletionStatus);
        Assert.IsTrue(_logger.LastMessage?.Contains("failure"));
    }

    [TestMethod]
    public void LogTracker_ExposesFormatProviderAndActiveTrackerCount()
    {
        // Arrange
        var activeTrackerCount = LogTracker.GetActiveTrackerCount();
        var tracker = new LogTracker("message", LogLevel.Warning);

        // Act
        var formatProvider = tracker.GetFormat(null);
        var updatedTrackerCount = LogTracker.GetActiveTrackerCount();
        tracker.Complete(CompletionStatus.Success);

        // Assert
        Assert.AreEqual("message", tracker.Message);
        Assert.AreEqual(LogLevel.Warning, tracker.LogLevel);
        Assert.AreSame(tracker, formatProvider);
        Assert.AreEqual(activeTrackerCount + 1, updatedTrackerCount);
    }
}
