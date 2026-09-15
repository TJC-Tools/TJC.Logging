using TJC.Logging.Settings.Format;
using TJC.Logging.Settings.Format.Specialty;

namespace TJC.Logging.Tests.Format;


[Collection("Logging")]


public class FormattingSettingsTests
{
    [Fact]
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
        Assert.Equal(state.DateTime.ToString("yyyy"), timestampResult);
        Assert.Equal(
            "[TJC.Logging.Tests.Format.FormattingSettingsTests.Member.7]",
            locationResult
        );
        Assert.Equal("<Get>", specialtyResult);
        Assert.Throws<NotImplementedException>(() => timestamp.ToString(null, null));
        Assert.Throws<NotImplementedException>(() => location.ToString(null, null));
        Assert.Throws<NotImplementedException>(() => specialty.ToString(null, null));
    }

    [Fact]
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
        Assert.Equal("[UNKNOWN_NAMESPACE|UNKNOWN_TYPE|Member|7]", result);
        Assert.NotNull(FormatLocationSettings.Default);
    }

    [Fact]
    public void FormattingSettings_IncludeAndExcludeFormatters()
    {
        // Arrange
        var settings = new FormattingSettings { Separator = "|" };
        var state = new TestLogState(
            SpecialtyLogTypes.None,
            typeof(FormattingSettingsTests),
            "Member",
            7
        );

        // Act
        settings.ExcludeAll();
        var excluded = settings.ToString(null, state);
        settings.IncludeAll();
        var included = settings.ToString(null, state);

        // Assert
        Assert.Equal(string.Empty, excluded);
        Assert.True(included.Contains("FormattingSettingsTests"));
        Assert.Throws<NotImplementedException>(() => settings.ToString(null, null));
        Assert.Throws<NotImplementedException>(() => settings.ToString());
    }

    [Fact]
    public void FormatSpecialty_ReturnsEmptyForNoSpecialty()
    {
        // Arrange
        var settings = new FormatSpecialtyLogTypeSettings();
        var state = new TestLogState(SpecialtyLogTypes.None, null, string.Empty, 0);

        // Act
        var result = settings.ToString(null, state);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetSetSettings_FormatsNullAndValues()
    {
        // Arrange
        var settings = new FormatGetSetSettings();

        // Act
        var get = settings.ToGetString<string>(null, "value");
        var set = settings.ToSetString("old", "new", "value");

        // Assert
        Assert.Equal("[value] is null", get);
        Assert.Equal("[value] from [old] to [new]", set);
    }

    [Fact]
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
        Assert.Equal(" Started", active);
        Assert.True(completed.StartsWith(" Success ["));
        Assert.Throws<NotImplementedException>(() => settings.ToString(null, null));
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
