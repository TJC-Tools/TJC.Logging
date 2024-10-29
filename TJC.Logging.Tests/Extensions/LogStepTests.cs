namespace TJC.Logging.Tests.Extensions;

[TestClass]
public class LogStepTests
{
    private readonly MockTraceLogger _logger = new();

    [TestInitialize]
    public void Initialize() => Settings.Settings.ReloadDefaults(); // Reset settings before each test

    [TestMethod]
    public void LogStep_MultipleCall_Increments()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();

        // Act
        var step = 0;
        _logger.LogStep(ref step);
        var step1 = _logger.LastMessage;
        _logger.LogStep(ref step);
        var step2 = _logger.LastMessage;
        _logger.LogStep(ref step);
        var step3 = _logger.LastMessage;

        // Assert
        Assert.AreEqual("Step 1", step1);
        Assert.AreEqual("Step 2", step2);
        Assert.AreEqual("Step 3", step3);
    }
}
