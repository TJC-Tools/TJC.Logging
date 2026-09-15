using TJC.Logging.Settings.Format;
using TJC.Logging.Settings.Format.Specialty;

namespace TJC.Logging.Tests.Format;

[TestClass]
public class FormattingSettingsTests
{
    [TestMethod]
    public void Formatters_UseStateAndRejectUnsupportedProviders()
    {
        // Arrange
        var state = new TestLogState(
            SpecialtyLogTypes.Get,
            typeof(FormattingSettingsTests),
            "Member",
            7
        );
        var timestamp = new FormatTimestampSettings { Format = "yyyy" };
        var location = new FormatLocationSettings(
            includeNamespace: true,
            includeType: true,
            includeMember: true,
            includeLineNumber: true
        );
        var specialty = new FormatSpecialtyLogTypeSettings(prefix: "<", suffix: ">");

        // Act
        var timestampResult = timestamp.ToString(null, state);
        var locationResult = location.ToString(null, state);
        var specialtyResult = specialty.ToString(null, state);

        // Assert
        Assert.AreEqual(state.DateTime.ToString("yyyy"), timestampResult);
        Assert.AreEqual("[TJC.Logging.Tests.Format.FormattingSettingsTests.Member.7]", locationResult);
        Assert.AreEqual("<Get>", specialtyResult);
        Assert.ThrowsException<NotImplementedException>(() => timestamp.ToString(null, null));
        Assert.ThrowsException<NotImplementedException>(() => location.ToString(null, null));
        Assert.ThrowsException<NotImplementedException>(() => specialty.ToString(null, null));
    }

    [TestMethod]
    public void FormatLocation_UsesUnknownNames_WhenCallingTypeIsUnavailable()
    {
        // Arrange
        var settings = new FormatLocationSettings(
            includeNamespace: true,
            includeType: true,
            includeMember: true,
            includeLineNumber: true,
            separator: "|"
        );
        var state = new TestLogState(SpecialtyLogTypes.None, null, "Member", 7);

        // Act
        var result = settings.ToString(null, state);

        // Assert
        Assert.AreEqual("[UNKNOWN_NAMESPACE|UNKNOWN_TYPE|Member|7]", result);
        Assert.IsNotNull(FormatLocationSettings.Default);
    }

    [TestMethod]
    public void FormattingSettings_IncludeAndExcludeFormatters()
    {
        // Arrange
        var settings = new FormattingSettings { Separator = "|" };
        var state = new TestLogState(SpecialtyLogTypes.None, typeof(FormattingSettingsTests), "Member", 7);

        // Act
        settings.ExcludeAll();
        var excluded = settings.ToString(null, state);
        settings.IncludeAll();
        var included = settings.ToString(null, state);

        // Assert
        Assert.AreEqual(string.Empty, excluded);
        Assert.IsTrue(included.Contains("FormattingSettingsTests"));
        Assert.ThrowsException<NotImplementedException>(() => settings.ToString(null, null));
        Assert.ThrowsException<NotImplementedException>(() => settings.ToString());
    }

    [TestMethod]
    public void FormatSpecialty_ReturnsEmptyForNoSpecialty()
    {
        // Arrange
        var settings = new FormatSpecialtyLogTypeSettings();
        var state = new TestLogState(SpecialtyLogTypes.None, null, string.Empty, 0);

        // Act
        var result = settings.ToString(null, state);

        // Assert
        Assert.AreEqual(string.Empty, result);
    }

    [TestMethod]
    public void GetSetSettings_FormatsNullAndValues()
    {
        // Arrange
        var settings = new FormatGetSetSettings();

        // Act
        var get = settings.ToGetString<string>(null, "value");
        var set = settings.ToSetString("old", "new", "value");

        // Assert
        Assert.AreEqual("[value] is null", get);
        Assert.AreEqual("[value] from [old] to [new]", set);
    }

    [TestMethod]
    public void TrackerSettings_FormatsActiveAndCompletedTrackers()
    {
        // Arrange
        var settings = new FormatTrackerSettings();
        var tracker = new LogTracker();

        // Act
        var active = settings.ToString(null, tracker);
        tracker.Complete(CompletionStatus.Success);
        var completed = settings.ToString(null, tracker);

        // Assert
        Assert.AreEqual(" Started", active);
        Assert.IsTrue(completed.StartsWith(" Success ["));
        Assert.ThrowsException<NotImplementedException>(() => settings.ToString(null, null));
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