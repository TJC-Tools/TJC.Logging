namespace TJC.Logging.Extensions.Specialty;

public static class LogSetExtension
{
    private static FormatGetSetSettings Settings =>
        Logging.Settings.Settings.Instance.Formatting.Specialty.GetSet;

    /// <summary>
    /// Log set of a property or variable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="logger"></param>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    /// <param name="argumentName"></param>
    /// <param name="memberName"></param>
    /// <param name="lineNumber"></param>
    /// <returns></returns>
    public static void LogSet<T>(this ILogger logger,
                                 ref T obj,
                                 T value,
                                 [CallerArgumentExpression(nameof(obj))]
                                 string argumentName = "",
                                 [CallerMemberName] string memberName = "",
                                 [CallerLineNumber] int lineNumber = -1)
    {
        logger.LogMetadata(logLevel: LogLevel.Trace,
                           specialtyLogType: SpecialtyLogTypes.Set,
                           message: Settings.ToSetString(obj, value, argumentName),
                           frameIndex: 1,
                           memberName: memberName,
                           lineNumber: lineNumber);
        obj = value;
    }
}