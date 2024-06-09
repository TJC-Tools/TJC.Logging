namespace TJC.Logging.States;

/// <summary>
/// Used to store the state of the log when called
/// </summary>
/// <param name="frameIndex">Set to how many frames up the initial call was</param>
/// <param name="memberName">Leave blank unless overriding with a value from an earlier frame</param>
/// <param name="lineNumber">Leave blank unless overriding with a value from an earlier frame</param>
internal class LogState(
    int                       frameIndex = 0,
    [CallerMemberName] string memberName = "",
    [CallerLineNumber] int    lineNumber = 0)
    : IFormattable
{
    #region Properties

    internal Type? CallingType { get; } = GetCallingType(frameIndex + 1);
    internal string MemberName { get; } = memberName;
    internal int LineNumber { get; } = lineNumber;

    #endregion

    #region Initialization

    private static Type? GetCallingType(int frameIndex)
    {
        var stackTrace = new StackTrace();
        var frame      = stackTrace.GetFrame(frameIndex + 1);
        var method     = frame?.GetMethod();
        var type       = method?.ReflectedType;
        return type;
    }

    #endregion

    #region IFormattable

    public override string ToString() =>
        ToString(null, null);

    public string ToString(string? format, IFormatProvider? formatProvider) =>
        this.FormatLocation();

    #endregion
}