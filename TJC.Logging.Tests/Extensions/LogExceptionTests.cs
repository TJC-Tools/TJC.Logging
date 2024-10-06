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
        // Act
        try
        {
            throw new Exception("ABC");
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
        }

        var result  = _logger.LastMessage ?? string.Empty;
        var result1 = result.Contains($"[{nameof(LogExceptionTests)}.{nameof(LogException_ThrowException_ABC)}]");
        var result2 = result.Contains("Type: System.Exception");
        var result3 = result.Contains("Message: ABC");
        var result4 = result.Contains("Source: TJC.Logging.Tests");
        var result5 = result.Contains($"Stack Trace: at TJC.Logging.Tests.Extensions.{nameof(LogExceptionTests)}.{nameof(LogException_ThrowException_ABC)}() in");
        var result6 = result.Contains($@"\{nameof(LogExceptionTests)}.cs:line");

        // Assert
        Assert.IsTrue(result1, "Calling class & method missing");
        Assert.IsTrue(result2, "Exception type missing");
        Assert.IsTrue(result3, "Message missing");
        Assert.IsTrue(result4, "Source missing");
        Assert.IsTrue(result5, "Stack Trace missing");
        Assert.IsTrue(result6, "Line missing");
    }
}