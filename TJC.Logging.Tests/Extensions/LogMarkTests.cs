namespace TJC.Logging.Tests.Extensions;


[Collection("Logging")]


public class LogMarkTests
{
    private readonly MockTraceLogger _logger = new();

    private static string ThisNamespace =>
        typeof(LogMarkTests).Namespace ?? nameof(TJC.Logging.Tests.Extensions);
    public LogMarkTests() => Logging.Settings.Settings.ReloadDefaults(); // Reset logger settings before each test

    [Fact]
    public void LogMark_PrintsLocationInformation()
    {
        // Arrange
        Logging.Settings.Settings.Instance.Formatting.IncludeAll();

        // Act
        _logger.LogMark();

        // Assert
        Assert.True(
            _logger.LastMessage is not null,
            $"{nameof(MockTraceLogger.LastMessage)} is null after calling {nameof(LogMarkExtension.LogMark)}"
        );
        Assert.True(
            _logger.LastMessage.Contains(ThisNamespace),
            $"{nameof(LogMarkExtension.LogMark)} does not include namespace"
        );
        Assert.True(
            _logger.LastMessage.Contains(nameof(LogMarkTests)),
            $"{nameof(LogMarkExtension.LogMark)} does not include type name"
        );
        Assert.True(
            _logger.LastMessage.Contains(nameof(LogMark_PrintsLocationInformation)),
            $"{nameof(LogMarkExtension.LogMark)} does not include member name"
        );
    }
}
