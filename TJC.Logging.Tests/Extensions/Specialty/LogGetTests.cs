namespace TJC.Logging.Tests.Extensions.Specialty;

[TestClass]
public class LogGetTests
{
    private readonly MockTraceLogger _logger = new();

    [TestInitialize]
    public void Initialize() =>
        Settings.Settings.ReloadDefaults(); // Reset logger settings before each test

    [TestMethod]
    public void GetNullTest()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Specialty.Include.Include();
        Settings.Settings.Instance.Formatting.Specialty.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.GetSet.ArgumentPrefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.GetSet.ArgumentSuffix = string.Empty;
        int? value1 = null;

        // Act
        var value2 = _logger.LogGet(value1);

        // Assert
        Assert.AreEqual(null, value2);
        var expected = string.Concat(nameof(SpecialtyLogTypes.Get), nameof(value1), " is null");
        Assert.AreEqual(expected, _logger.LastMessage);
    }

    [TestMethod]
    public void GetIntTest()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Specialty.Include.Include();
        Settings.Settings.Instance.Formatting.Specialty.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.GetSet.ArgumentPrefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.GetSet.ArgumentSuffix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.GetSet.ValuePrefix = string.Empty;
        Settings.Settings.Instance.Formatting.Specialty.GetSet.ValueSuffix = string.Empty;
        const int value1 = 6;

        // Act
        var value2 = _logger.LogGet(value1);

        // Assert
        Assert.AreEqual(value1, value2);
        var expected = string.Concat(nameof(SpecialtyLogTypes.Get), nameof(value1), value1.ToString());
        Assert.AreEqual(expected, _logger.LastMessage);
    }
}