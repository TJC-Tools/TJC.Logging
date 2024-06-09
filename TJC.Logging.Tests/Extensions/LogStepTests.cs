namespace TJC.Logging.Tests.Extensions;

[TestClass]
public class LogStepTests
{
    private readonly MockTraceLogger _logger = new();

    [TestInitialize]
    public void Initialize() =>
        Settings.Settings.ReloadDefaults();

    [TestMethod]
    public void LogStepTest()
    {
        Settings.Settings.Instance.Formatting.ExcludeAll();
        _logger.LogStep(1);
        Assert.AreEqual("Step 1", _logger.LastMessage);
        _logger.LogStep(2);
        Assert.AreEqual("Step 2", _logger.LastMessage);
    }
}