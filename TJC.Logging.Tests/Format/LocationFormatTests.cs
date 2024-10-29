namespace TJC.Logging.Tests.Format;

[TestClass]
public class LocationFormatTests
{
    private readonly MockTraceLogger _logger = new();

    [TestInitialize]
    public void Initialize() => Settings.Settings.ReloadDefaults(); // Reset settings before each test

    [TestMethod]
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
        Assert.AreEqual(location, _logger.LastMessage);
    }

    [TestMethod]
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
        Assert.AreEqual("45", _logger.LastMessage);
    }

    [TestMethod]
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
        Assert.AreEqual(typeof(LocationFormatTests).Namespace, _logger.LastMessage);
    }

    [TestMethod]
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
        Assert.AreEqual(nameof(LocationFormatTests), _logger.LastMessage);
    }

    [TestMethod]
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
        Assert.AreEqual(nameof(LogMark_IncludeMemberNameOnly), _logger.LastMessage);
    }

    [TestMethod]
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
        Assert.AreEqual(location, _logger.LastMessage);
    }

    [TestMethod]
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
        Assert.AreEqual(location, _logger.LastMessage);
    }

    [TestMethod]
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
        Assert.AreEqual(location, _logger.LastMessage);
    }
}
