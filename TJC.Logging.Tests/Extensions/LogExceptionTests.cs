namespace TJC.Logging.Tests.Extensions;

[Collection("Logging")]
public class LogExceptionTests
{
    private readonly MockTraceLogger _logger = new();

    public LogExceptionTests() => Settings.Settings.ReloadDefaults(); // Reset settings before each test

    [Fact]
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

        var result = _logger.LastMessage ?? string.Empty;
        var result1 = result.Contains(
            $"[{nameof(LogExceptionTests)}.{nameof(LogException_ThrowException_ABC)}]"
        );
        var result2 = result.Contains("Type: System.Exception");
        var result3 = result.Contains("Message: ABC");
        var result4 = result.Contains("Source: TJC.Logging.Tests");
        var result5 = result.Contains(
            $"Stack Trace: at TJC.Logging.Tests.Extensions.{nameof(LogExceptionTests)}.{nameof(LogException_ThrowException_ABC)}() in"
        );
        var result6 = result.Contains($@"\{nameof(LogExceptionTests)}.cs:line");

        // Assert
        Assert.True(result1, "Calling class & method missing");
        Assert.True(result2, "Exception type missing");
        Assert.True(result3, "Message missing");
        Assert.True(result4, "Source missing");
        Assert.True(result5, "Stack Trace missing");
        Assert.True(result6, "Line missing");
    }

    [Fact]
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

        var result = _logger.LastMessage ?? string.Empty;
        var result1 = result.Contains("Inner Exception:");

        // Assert
        Assert.False(result1, "Inner exception present when it shouldn't be");
    }

    [Fact]
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

        var result = _logger.LastMessage ?? string.Empty;
        var result1 = result.Contains("Inner Exception: System.Exception: DEF");

        // Assert
        Assert.True(result1, "Inner exception missing");
    }

    [Fact]
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

        var result = _logger.LastMessage ?? string.Empty;
        var result1 = result.Contains(
            $"\r\n\t\tat TJC.Logging.Tests.Mocks.{nameof(MockExceptionThrower)}.{nameof(MockExceptionThrower.ThrowException)}(String message) in"
        );
        var result2 = result.Contains(
            $"\r\n\t\tat TJC.Logging.Tests.Mocks.{nameof(MockExceptionThrower)}.{nameof(MockExceptionThrower.CallMethodToThrowException)}(String message) in"
        );
        var result3 = result.Contains(
            $"\r\n\t\tat TJC.Logging.Tests.Extensions.{nameof(LogExceptionTests)}.{nameof(LogException_ThrowException_WithStackTrace)}() in"
        );

        // Assert
        Assert.True(result1, "Stack trace new-lines with tabs not matching expected");
        Assert.True(result2, "Stack trace new-lines with tabs not matching expected");
        Assert.True(result3, "Stack trace new-lines with tabs not matching expected");
    }

    [Fact]
    public void LogException_WithoutStackTrace_LogsEmptyStackTrace()
    {
        // Act
        _logger.LogException(new Exception("ABC"));

        // Assert
        Assert.True(_logger.LastMessage?.Contains("Stack Trace: "));
    }
}
