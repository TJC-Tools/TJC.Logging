namespace TJC.Logging.Interfaces;

internal interface IPrimaryFormatterSettings : IIncludable, IFormattable
{
    Inclusion.Inclusion Include { get; set; }

    Priority.Priority Priority { get; set; }
}
