namespace TJC.Logging.Tests.Extensions.Specialty;

[Collection("Logging")]
public class LogGetTests
{
    private readonly MockTraceLogger _logger = new();

    public LogGetTests() => Settings.Settings.ReloadDefaults(); // Reset logger settings before each test

    [Fact]
    public void LogGet_IsNull()
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
        Assert.Null(value2);
        var expected = string.Concat(nameof(SpecialtyLogTypes.Get), nameof(value1), " is null");
        Assert.Equal(expected, _logger.LastMessage);
    }

    [Fact]
    public void LogGet_IntIs6()
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
        Assert.Equal(value1, value2);
        var expected = string.Concat(
            nameof(SpecialtyLogTypes.Get),
            nameof(value1),
            value1.ToString()
        );
        Assert.Equal(expected, _logger.LastMessage);
    }
}
