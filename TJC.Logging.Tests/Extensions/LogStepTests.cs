namespace TJC.Logging.Tests.Extensions;

[Collection("Logging")]
public class LogStepTests
{
    private readonly MockTraceLogger _logger = new();

    public LogStepTests() => Settings.Settings.ReloadDefaults(); // Reset settings before each test

    [Fact]
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
        Assert.Equal("Step 1", step1);
        Assert.Equal("Step 2", step2);
        Assert.Equal("Step 3", step3);
    }

    [Fact]
    public void LogStep_ValueOverload_LogsProvidedStepWithoutMutation()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        const int step = 4;

        // Act
        _logger.LogStep(step);

        // Assert
        Assert.Equal(4, step);
        Assert.Equal("Step 4", _logger.LastMessage);
    }
}
