using TJC.Logging.Settings.Format;

namespace TJC.Logging.Tests.Format;

[TestClass]
public class FormatMarkdownSettingsTests
{
    private readonly MockMarkdownLogger _logger = new();

    [TestInitialize]
    public void Initialize()
    {
        TJC.Logging.Settings.Settings.ReloadDefaults();
        var settings = new FormatMarkdownSettings();
        settings.Timestamp.Format = "yyyy";
        TJC.Logging.Settings.Settings.Instance.Formatting = settings;
    }

    [TestMethod]
    public void LogMessage_WritesMarkdownCalloutToFile()
    {
        // Act
        _logger.LogMessage("ABC");
        var result = File.ReadAllText(MockMarkdownLogger.LogFilePath);

        // Assert
        Assert.AreEqual(_logger.LastMessage, result);
        StringAssert.StartsWith(
            result,
            $"> [!QUOTE]- [{nameof(FormatMarkdownSettingsTests)}.{nameof(LogMessage_WritesMarkdownCalloutToFile)}]{Environment.NewLine}> {DateTime.Now:yyyy}{Environment.NewLine}> ABC{Environment.NewLine}{Environment.NewLine}"
        );
    }

    [TestMethod]
    public void FormatLog_WritesExceptionAsCalloutLine()
    {
        // Arrange
        var state = new TestLogState(
            SpecialtyLogTypes.None,
            typeof(FormatMarkdownSettingsTests),
            "Member",
            7
        );
        var settings = new FormatMarkdownSettings();
        settings.Timestamp.Format = "yyyy";

        // Act
        var result = settings.FormatLog(state, "message", new Exception("failure"), LogLevel.Error);

        // Assert
        Assert.AreEqual(
            $"> [!FAIL]- [{nameof(FormatMarkdownSettingsTests)}.Member]{Environment.NewLine}> 2026{Environment.NewLine}> message{Environment.NewLine}> Exception: failure{Environment.NewLine}{Environment.NewLine}",
            result
        );
    }

    [TestMethod]
    public void FormatLog_UsesConfiguredTitleAndContentTemplates()
    {
        // Arrange
        var state = new TestLogState(
            SpecialtyLogTypes.None,
            typeof(FormatMarkdownSettingsTests),
            "Member",
            7
        );
        var settings = new FormatMarkdownSettings
        {
            TitleFormat = "{Message} at {Title}",
            ContentFormat = "When: {Timestamp}\nIssue: {Exception}\nDetails: {Message}",
        };
        settings.Timestamp.Format = "yyyy";

        // Act
        var result = settings.FormatLog(state, "message", new Exception("failure"), LogLevel.Error);

        // Assert
        Assert.AreEqual(
            $"> [!FAIL]- message at [{nameof(FormatMarkdownSettingsTests)}.Member]{Environment.NewLine}> When: 2026{Environment.NewLine}> Issue: failure{Environment.NewLine}> Details: message{Environment.NewLine}{Environment.NewLine}",
            result
        );
    }

    [TestMethod]
    public void FormatLog_ExpandedCalloutOmitsEmptyExceptionLine()
    {
        var state = new TestLogState(SpecialtyLogTypes.None, null, "", 0);
        var settings = new FormatMarkdownSettings
        {
            IsCollapsed = false,
            ContentFormat = "{Message}\nException: {Exception}",
        };

        var result = settings.FormatLog(state, "message", null, LogLevel.None);

        Assert.AreEqual(
            $"> [!NOTE] [UNKNOWN_TYPE.]{Environment.NewLine}> message{Environment.NewLine}{Environment.NewLine}",
            result
        );
    }

    [TestMethod]
    public void FormatLog_UsesCalloutTypeForLogLevel()
    {
        // Arrange
        var state = new TestLogState(SpecialtyLogTypes.None, null, "Member", 7);
        var settings = new FormatMarkdownSettings();

        // Act and Assert
        AssertCalloutType(LogLevel.Trace, "QUOTE");
        AssertCalloutType(LogLevel.Debug, "QUOTE");
        AssertCalloutType(LogLevel.Information, "INFO");
        AssertCalloutType(LogLevel.Warning, "WARNING");
        AssertCalloutType(LogLevel.Error, "FAIL");
        AssertCalloutType(LogLevel.Critical, "DANGER");

        void AssertCalloutType(LogLevel logLevel, string calloutType) =>
            StringAssert.StartsWith(
                settings.FormatLog(state, "message", null, logLevel),
                $"> [!{calloutType}]- "
            );
    }

    private sealed class TestLogState(
        SpecialtyLogTypes specialty,
        Type? callingType,
        string memberName,
        int lineNumber
    ) : TJC.Logging.Interfaces.ILogState
    {
        public DateTime DateTime { get; } = new(2026, 1, 2, 3, 4, 5);
        public SpecialtyLogTypes Specialty { get; } = specialty;
        public Type? CallingType { get; } = callingType;
        public string MemberName { get; } = memberName;
        public int LineNumber { get; } = lineNumber;

        public object? GetFormat(Type? formatType) => this;

        public string ToString(string? format, IFormatProvider? formatProvider) => string.Empty;
    }
}
