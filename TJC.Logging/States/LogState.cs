namespace TJC.Logging.States;

/// <summary>
/// Used to store the state of the log when called
/// </summary>
/// <param name="frameIndex">Set to how many frames up the initial call was</param>
/// <param name="specialtyLogType">Specialty log type</param>
/// <param name="memberName">Leave blank unless overriding with a value from an earlier frame</param>
/// <param name="lineNumber">Leave blank unless overriding with a value from an earlier frame</param>
internal class LogState(
    int frameIndex = 0,
    SpecialtyLogTypes specialtyLogType = SpecialtyLogTypes.None,
    string memberName = "",
    int lineNumber = 0
) : ILogState
{
    #region Properties

    public DateTime DateTime { get; } = DateTime.Now;
    public SpecialtyLogTypes Specialty { get; } = specialtyLogType;
    public Type? CallingType { get; } = GetCallingType(frameIndex + 1);
    public string MemberName { get; } = memberName;
    public int LineNumber { get; } = lineNumber;

    #endregion

    #region Initialization

    private static Type? GetCallingType(int frameIndex)
    {
        var stackTrace = new StackTrace();
        var frame = stackTrace.GetFrame(frameIndex + 1);
        var method = frame?.GetMethod();
        var type = method?.ReflectedType;
        return type;
    }

    #endregion

    #region IFormattable

    public override string ToString() => ToString(null, null);

    public string ToString(string? format, IFormatProvider? formatProvider) =>
        Settings.Settings.Instance.Formatting.ToString(format, this);

    #endregion

    #region IFormatProvider

    public object? GetFormat(Type? formatType) => this;

    #endregion
}
