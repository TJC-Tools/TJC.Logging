namespace TJC.Logging.Extensions.Specialty;

public static class LogGetExtension
{
    private static FormatGetSetSettings Settings =>
        Logging.Settings.Settings.Instance.Formatting.Specialty.GetSet;

    /// <summary>
    /// Log
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="logger"></param>
    /// <param name="obj"></param>
    /// <param name="argumentName"></param>
    /// <param name="memberName"></param>
    /// <param name="lineNumber"></param>
    /// <returns></returns>
    public static T LogGet<T>(this ILogger logger,
                              T obj,
                              [CallerArgumentExpression(nameof(obj))]
                              string argumentName = "",
                              [CallerMemberName] string memberName = "",
                              [CallerLineNumber] int lineNumber = -1)
    {
        logger.LogMetadata(logLevel: LogLevel.Trace,
                           specialtyLogType: SpecialtyLogTypes.Get,
                           message: Settings.ToGetString(obj, argumentName),
                           frameIndex: 1,
                           memberName: memberName,
                           lineNumber: lineNumber);
        return obj;
    }
}