namespace TJC.Logging.Tests.Format;

[TestClass]
public class LocationFormatTests
{
    private readonly MockTraceLogger _logger = new();

    [TestInitialize]
    public void Initialize() =>
        Settings.Settings.ReloadDefaults();

    [TestMethod]
    public void IncludeLineNumberOnly()
    {
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeLineNumber = true;
        _logger.Mark();
        Assert.AreEqual("[21]", _logger.LastMessage);
    }

    [TestMethod]
    public void IncludeTypeMemberAndLineNumber()
    {
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.IncludeAll();
        _logger.Mark();
        Assert.AreEqual($"[{typeof(LocationFormatTests).Namespace}.{nameof(LocationFormatTests)}.{nameof(IncludeTypeMemberAndLineNumber)}.30]", _logger.LastMessage);
    }

    [TestMethod]
    public void IncludeTypeNameOnly()
    {
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeType = true;
        _logger.Mark();
        Assert.AreEqual($"[{nameof(LocationFormatTests)}]", _logger.LastMessage);
    }

    [TestMethod]
    public void IncludeMemberNameOnly()
    {
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeMember = true;
        _logger.Mark();
        Assert.AreEqual($"[{nameof(IncludeMemberNameOnly)}]", _logger.LastMessage);
    }

    [TestMethod]
    public void IncludeTypeAndMemberName()
    {
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeType = true;
        Settings.Settings.Instance.Formatting.Location.IncludeMember = true;
        _logger.Mark();
        Assert.AreEqual($"[{nameof(LocationFormatTests)}.{nameof(IncludeTypeAndMemberName)}]", _logger.LastMessage);
    }

    [TestMethod]
    public void IncludeNamespaceTypeAndMemberName()
    {
        Settings.Settings.Instance.Formatting.ExcludeAll();
        Settings.Settings.Instance.Formatting.Location.Include = true;
        Settings.Settings.Instance.Formatting.Location.IncludeNamespace = true;
        Settings.Settings.Instance.Formatting.Location.IncludeType = true;
        Settings.Settings.Instance.Formatting.Location.IncludeMember = true;
        _logger.Mark();
        Assert.AreEqual($"[{typeof(LocationFormatTests).Namespace}.{nameof(LocationFormatTests)}.{nameof(IncludeNamespaceTypeAndMemberName)}]", _logger.LastMessage);
    }
}