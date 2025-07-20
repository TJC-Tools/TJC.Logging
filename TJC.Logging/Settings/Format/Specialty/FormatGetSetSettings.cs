namespace TJC.Logging.Settings.Format.Specialty;

/// <summary>
/// Formatting settings for <seealso cref="LogGetExtension"/> &amp; <seealso cref="LogSetExtension"/>.
/// </summary>
/// <param name="argumentPrefix"></param>
/// <param name="argumentSuffix"></param>
/// <param name="valuePrefix"></param>
/// <param name="valueSuffix"></param>
/// <param name="nullPrefix"></param>
/// <param name="nullSuffix"></param>
/// <param name="from"></param>
/// <param name="to"></param>
/// <param name="nullName"></param>
public class FormatGetSetSettings(
    string argumentPrefix = "[",
    string argumentSuffix = "]",
    string valuePrefix = "[",
    string valueSuffix = "]",
    string nullPrefix = " is ",
    string nullSuffix = "",
    string from = " from ",
    string to = " to ",
    string nullName = "null"
)
{
    #region Properties

    /// <summary>
    /// Argument prefix.
    /// </summary>
    public string ArgumentPrefix { get; set; } = argumentPrefix;

    /// <summary>
    /// Argument suffix.
    /// </summary>
    public string ArgumentSuffix { get; set; } = argumentSuffix;

    /// <summary>
    /// Value prefix.
    /// </summary>
    public string ValuePrefix { get; set; } = valuePrefix;

    /// <summary>
    /// Value suffix.
    /// </summary>
    public string ValueSuffix { get; set; } = valueSuffix;

    /// <summary>
    /// Null prefix.
    /// </summary>
    public string NullPrefix { get; set; } = nullPrefix;

    /// <summary>
    /// Null suffix.
    /// </summary>
    public string NullSuffix { get; set; } = nullSuffix;

    /// <summary>
    /// Word indicating what something was set from.
    /// </summary>
    public string From { get; set; } = from;

    /// <summary>
    /// Word indicating what something was set to.
    /// </summary>
    public string To { get; set; } = to;

    /// <summary>
    /// Word if set to or from null.
    /// </summary>
    public string Null { get; set; } = nullName;

    #endregion

    #region Methods

    internal string ToGetString<T>(
        T? obj,
        [CallerArgumentExpression(nameof(obj))] string argumentName = ""
    ) => string.Concat(ArgumentPrefix, argumentName, ArgumentSuffix, ToStringOrNull(obj));

    internal string ToSetString<T>(
        T? obj,
        T? value,
        [CallerArgumentExpression(nameof(obj))] string argumentName = ""
    ) =>
        string.Concat(
            ArgumentPrefix,
            argumentName,
            ArgumentSuffix,
            From,
            ToStringOrNull(obj),
            To,
            ToStringOrNull(value)
        );

    private string ToStringOrNull(object? obj) =>
        obj == null
            ? string.Concat(NullPrefix, Null, NullSuffix)
            : string.Concat(ValuePrefix, obj, ValueSuffix);

    #endregion
}
