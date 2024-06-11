namespace TJC.Logging.Interfaces;

internal interface IFormatterSettings : IFormattable
{
    Inclusion.Inclusion Include { get; set; }

    Priority.Priority Priority { get; set; }
}