namespace TJC.Logging.Settings.Format.Specialty;

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

    public string ArgumentPrefix { get; set; } = argumentPrefix;

    public string ArgumentSuffix { get; set; } = argumentSuffix;

    public string ValuePrefix { get; set; } = valuePrefix;

    public string ValueSuffix { get; set; } = valueSuffix;

    public string NullPrefix { get; set; } = nullPrefix;

    public string NullSuffix { get; set; } = nullSuffix;

    public string From { get; set; } = from;

    public string To { get; set; } = to;

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
