namespace TJC.Logging.Tests.Extensions.Specialty;


[Collection("Logging")]


public class LogTrackerTests
{
    private readonly MockTraceLogger _logger = new();
    public LogTrackerTests() => Settings.Settings.ReloadDefaults();

    [Fact]
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
        Assert.Equal(message, _logger.LastMessage);
        Assert.NotNull(tracker);
        Assert.Equal(CompletionStatus.Started, tracker.CompletionStatus);
        Assert.True(LogTracker.Trackers.Contains(tracker));
    }

    [Fact]
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
        Assert.True(_logger.LastMessage?.StartsWith(message));
        Assert.Equal(CompletionStatus.Success, tracker.CompletionStatus);
        Assert.NotNull(tracker.EndTime);
        Assert.False(LogTracker.Trackers.Contains(tracker));
    }

    [Fact]
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
        Assert.True(_logger.LastMessage?.StartsWith(message));
        Assert.Equal(CompletionStatus.Failure, tracker.CompletionStatus);
        Assert.NotNull(tracker.EndTime);
        Assert.False(LogTracker.Trackers.Contains(tracker));
    }

    [Fact]
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
        Assert.True(_logger.LastMessage?.StartsWith(message));
    }

    [Fact]
    public void LogEnd_ShouldThrowArgumentException_ForStartedStatus()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Specialty.Include.Include();
        Settings.Settings.Instance.Formatting.Specialty.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Tracker.Separator = string.Empty;
        var tracker = new LogTracker();

        // Act and Assert
        Assert.Throws<ArgumentException>(() => _logger.LogEnd(tracker, CompletionStatus.Started));
    }

    [Fact]
    public void LogSuccess_ShouldCompleteTracker()
    {
        // Arrange
        var tracker = new LogTracker();

        // Act
        _logger.LogSuccess(tracker);

        // Assert
        Assert.Equal(CompletionStatus.Success, tracker.CompletionStatus);
        Assert.NotNull(tracker.EndTime);
    }

    [Fact]
    public void LogFail_ShouldCompleteTrackerAndLogException()
    {
        // Arrange
        var tracker = new LogTracker();
        var exception = new InvalidOperationException("failure");

        // Act
        _logger.LogFail(tracker, exception);

        // Assert
        Assert.Equal(CompletionStatus.Failure, tracker.CompletionStatus);
        Assert.True(_logger.LastMessage?.Contains("failure"));
    }

    [Fact]
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
        Assert.Equal("message", tracker.Message);
        Assert.Equal(LogLevel.Warning, tracker.LogLevel);
        Assert.Same(tracker, formatProvider);
        Assert.Equal(activeTrackerCount + 1, updatedTrackerCount);
    }
}
