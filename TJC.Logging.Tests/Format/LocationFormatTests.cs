namespace TJC.Logging.Tests.Format;

[Collection("Logging")]
public class LocationFormatTests
{
    private readonly MockTraceLogger _logger = new();

    public LocationFormatTests() => Settings.Settings.ReloadDefaults(); // Reset settings before each test

    [Fact]
    public void LogMark_IncludeAll()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.IncludeAll();
        Settings.Settings.Instance.Formatting.Location.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Separator = string.Empty;

        // Act
        _logger.LogMark();

        // Assert
        var location = string.Concat(
            typeof(LocationFormatTests).Namespace,
            nameof(LocationFormatTests),
            nameof(LogMark_IncludeAll),
            "22"
        );
        Assert.Equal(location, _logger.LastMessage);
    }

    [Fact]
    public void LogMark_IncludeLineNumberOnly()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeLineNumber = true;
        Settings.Settings.Instance.Formatting.Location.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Suffix = string.Empty;

        // Act
        _logger.LogMark();

        // Assert
        Assert.Equal("45", _logger.LastMessage);
    }

    [Fact]
    public void LogMark_IncludeNamespaceOnly()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeNamespace = true;
        Settings.Settings.Instance.Formatting.Location.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Suffix = string.Empty;

        // Act
        _logger.LogMark();

        // Assert
        Assert.Equal(typeof(LocationFormatTests).Namespace, _logger.LastMessage);
    }

    [Fact]
    public void LogMark_IncludeTypeNameOnly()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeType = true;
        Settings.Settings.Instance.Formatting.Location.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Suffix = string.Empty;

        // Act
        _logger.LogMark();

        // Assert
        Assert.Equal(nameof(LocationFormatTests), _logger.LastMessage);
    }

    [Fact]
    public void LogMark_IncludeMemberNameOnly()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeMember = true;
        Settings.Settings.Instance.Formatting.Location.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Suffix = string.Empty;

        // Act
        _logger.LogMark();

        // Assert
        Assert.Equal(nameof(LogMark_IncludeMemberNameOnly), _logger.LastMessage);
    }

    [Fact]
    public void IncludeTypeAndMemberName()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeType = true;
        Settings.Settings.Instance.Formatting.Location.IncludeMember = true;
        Settings.Settings.Instance.Formatting.Location.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Separator = string.Empty;

        // Act
        _logger.LogMark();

        // Assert
        var location = string.Concat(nameof(LocationFormatTests), nameof(IncludeTypeAndMemberName));
        Assert.Equal(location, _logger.LastMessage);
    }

    [Fact]
    public void LogMark_IncludeTypeAndMemberNameOnly()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeType = true;
        Settings.Settings.Instance.Formatting.Location.IncludeMember = true;
        Settings.Settings.Instance.Formatting.Location.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Separator = string.Empty;

        // Act
        _logger.LogMark();

        // Assert
        var location = string.Concat(
            nameof(LocationFormatTests),
            nameof(LogMark_IncludeTypeAndMemberNameOnly)
        );
        Assert.Equal(location, _logger.LastMessage);
    }

    [Fact]
    public void LogMark_IncludeNamespaceTypeAndMemberOnly()
    {
        // Arrange
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeNamespace = true;
        Settings.Settings.Instance.Formatting.Location.IncludeType = true;
        Settings.Settings.Instance.Formatting.Location.IncludeMember = true;
        Settings.Settings.Instance.Formatting.Location.Prefix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Suffix = string.Empty;
        Settings.Settings.Instance.Formatting.Location.Separator = string.Empty;

        // Act
        _logger.LogMark();

        // Assert
        var location = string.Concat(
            typeof(LocationFormatTests).Namespace,
            nameof(LocationFormatTests),
            nameof(LogMark_IncludeNamespaceTypeAndMemberOnly)
        );
        Assert.Equal(location, _logger.LastMessage);
    }
}
