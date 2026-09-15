namespace TJC.Logging.Tests.Extensions.Specialty;


[Collection("Logging")]


public class LogSetTests
{
    private readonly MockTraceLogger _logger = new();
    public LogSetTests() => Settings.Settings.ReloadDefaults(); // Reset logger settings before each test

    [Fact]
    public void LogSet_IntFrom7To8()
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
        const int before = 7;
        const int after = 8;
        var num = before;

        // Act
        _logger.LogSet(ref num, after);

        // Assert
        Assert.Equal(after, num);
        var expected = string.Concat(
            nameof(SpecialtyLogTypes.Set),
            nameof(num),
            " from ",
            before.ToString(),
            " to ",
            after.ToString()
        );
        Assert.Equal(expected, _logger.LastMessage);
    }
}
