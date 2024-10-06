namespace TJC.Logging.Tests.Extensions;

[TestClass]
public class LogExceptionTests
{
    private readonly MockTraceLogger _logger = new();

    [TestInitialize]
    public void Initialize() =>
        Settings.Settings.ReloadDefaults(); // Reset settings before each test

    [TestMethod]
    public void LogException_ThrowException_ABC()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();

        // Act
        try
        {
            throw new Exception("ABC");
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
        }

        var result = _logger.LastMessage ?? string.Empty;
        var result1 = result.StartsWith("System.Exception: ABC");
        var result2 = result.Contains("at TJC.Logging.Tests.Extensions.LogExceptionTests.LogException_ThrowException_ABC() in");
        var result3 = result.EndsWith(@"\LogExceptionTests.cs:line 21");

        // Assert
        Assert.IsTrue(result1);
        Assert.IsTrue(result2);
        Assert.IsTrue(result3);
    }
}