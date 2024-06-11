namespace TJC.Logging.Interfaces;

internal interface ILogState : IFormattable, IFormatProvider
{
    Type? CallingType { get; }
    string MemberName { get; }
    int LineNumber { get; }
}