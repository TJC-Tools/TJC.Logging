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

    [TestMethod]
    public void LogException_ThrowException_WithoutInnerException()
    {
        // Act
        try
        {
            MockExceptionThrower.CallMethodToThrowException("ABC");
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
        }

        var result  = _logger.LastMessage ?? string.Empty;
        var result1 = result.Contains("Inner Exception:");

        // Assert
        Assert.IsFalse(result1, "Inner exception present when it shouldn't be");
    }

    [TestMethod]
    public void LogException_ThrowException_WithInnerException()
    {
        // Act
        try
        {
            MockExceptionThrower.CallMethodToThrowExceptionWithInnerException("ABC", "DEF");
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
        }

        var result  = _logger.LastMessage ?? string.Empty;
        var result1 = result.Contains("Inner Exception: System.Exception: DEF");

        // Assert
        Assert.IsTrue(result1, "Inner exception missing");
    }

    [TestMethod]
    public void LogException_ThrowException_WithStackTrace()
    {
        // Act
        try
        {
            MockExceptionThrower.CallMethodToThrowException("ABC");
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
        }

        var result  = _logger.LastMessage ?? string.Empty;
        var result1 = result.Contains($"\r\n\t\tat TJC.Logging.Tests.Mocks.{nameof(MockExceptionThrower)}.{nameof(MockExceptionThrower.ThrowException)}(String message) in");
        var result2 = result.Contains($"\r\n\t\tat TJC.Logging.Tests.Mocks.{nameof(MockExceptionThrower)}.{nameof(MockExceptionThrower.CallMethodToThrowException)}(String message) in");
        var result3 = result.Contains($"\r\n\t\tat TJC.Logging.Tests.Extensions.{nameof(LogExceptionTests)}.{nameof(LogException_ThrowException_WithStackTrace)}() in");

        // Assert
        Assert.IsTrue(result1, "Stack trace new-lines with tabs not matching expected");
        Assert.IsTrue(result2, "Stack trace new-lines with tabs not matching expected");
        Assert.IsTrue(result3, "Stack trace new-lines with tabs not matching expected");
    }
}