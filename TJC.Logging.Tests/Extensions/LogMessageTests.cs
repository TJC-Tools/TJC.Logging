namespace TJC.Logging.Tests.Extensions;

[TestClass]
public class LogMessageTests
{
    private readonly MockTraceLogger _logger = new();

    [TestInitialize]
    public void Initialize() =>
        Settings.Settings.ReloadDefaults(); // Reset settings before each test

    [TestMethod]
    public void LogMessage_ABC()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        var message = "ABC";

        // Act
        _logger.LogMessage(message);
        var result = _logger.LastMessage;

        // Assert
        Assert.AreEqual(message, result);
    }
}