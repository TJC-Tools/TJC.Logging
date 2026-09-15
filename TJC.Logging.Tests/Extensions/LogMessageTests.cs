namespace TJC.Logging.Tests.Extensions;


[Collection("Logging")]


public class LogMessageTests
{
    private readonly MockTraceLogger _logger = new();
    public LogMessageTests() => Settings.Settings.ReloadDefaults(); // Reset settings before each test

    [Fact]
    public void LogMessage_ABC()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        var message = "ABC";

        // Act
        _logger.LogMessage(message);
        var result = _logger.LastMessage;

        // Assert
        Assert.Equal(message, result);
    }
}
