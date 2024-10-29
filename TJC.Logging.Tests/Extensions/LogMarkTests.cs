namespace TJC.Logging.Tests.Extensions;

[TestClass]
public class LogMarkTests
{
    private readonly MockTraceLogger _logger = new();

    private static string ThisNamespace =>
        typeof(LogMarkTests).Namespace ?? nameof(TJC.Logging.Tests.Extensions);

    [TestInitialize]
    public void Initialize() => Logging.Settings.Settings.ReloadDefaults(); // Reset logger settings before each test

    [TestMethod]
    public void LogMark_PrintsLocationInformation()
    {
        // Arrange
        Logging.Settings.Settings.Instance.Formatting.IncludeAll();

        // Act
        _logger.LogMark();

        // Assert
        Assert.IsNotNull(
            _logger.LastMessage,
            $"{nameof(MockTraceLogger.LastMessage)} is null after calling {nameof(LogMarkExtension.LogMark)}"
        );
        Assert.IsTrue(
            _logger.LastMessage.Contains(ThisNamespace),
            $"{nameof(LogMarkExtension.LogMark)} does not include namespace"
        );
        Assert.IsTrue(
            _logger.LastMessage.Contains(nameof(LogMarkTests)),
            $"{nameof(LogMarkExtension.LogMark)} does not include type name"
        );
        Assert.IsTrue(
            _logger.LastMessage.Contains(nameof(LogMark_PrintsLocationInformation)),
            $"{nameof(LogMarkExtension.LogMark)} does not include member name"
        );
    }
}
