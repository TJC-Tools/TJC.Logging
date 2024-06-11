namespace TJC.Logging.Tests.Format;

[TestClass]
public class TimestampFormatTests
{
    private readonly MockTraceLogger _logger = new();

    [TestInitialize]
    public void Initialize() =>
        Logging.Settings.Settings.ReloadDefaults(); // Reset logger settings before each test

    [TestMethod]
    public void LogMark_IncludeTimestampOnly()
    {
        // Arrange
        const string format = "yyyy-mm-ddTHH:mm:ss";
        Logging.Settings.Settings.Instance.Formatting.ExcludeAll();
        Logging.Settings.Settings.Instance.Formatting.Timestamp.Include = true;
        Logging.Settings.Settings.Instance.Formatting.Timestamp.Format = format;

        // Act
        var before = DateTime.Now.ToString(format);
        _logger.LogMark();
        var after = DateTime.Now.ToString(format);

        // Assert
        // Compare the log to the time before & after in case the second changes while logging
        var logDateValid = _logger.LastMessage is not null && (_logger.LastMessage.Equals(before) || _logger.LastMessage.Equals(after));
        Assert.IsTrue(logDateValid, $"[{_logger.LastMessage}] does not match [Before: {before}] OR [After: {after}]");
    }
}