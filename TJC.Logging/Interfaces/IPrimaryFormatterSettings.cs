namespace TJC.Logging.Interfaces;

internal interface IPrimaryFormatterSettings : IFormattable
{
    Inclusion.Inclusion Include { get; set; }

    Priority.Priority Priority { get; set; }
}