namespace TJC.Logging.Interfaces;

internal interface ILogState : IFormattable, IFormatProvider
{
    DateTime DateTime { get; }
    Type? CallingType { get; }
    string MemberName { get; }
    int LineNumber { get; }
}