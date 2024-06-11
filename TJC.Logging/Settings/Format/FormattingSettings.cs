namespace TJC.Logging.Settings.Format;

public class FormattingSettings : IFormattable
{
    #region Properties

    public string Separator { get; set; } = string.Empty;

    public FormatLocationSettings Location { get; set; } = new();

    #endregion

    #region Methods

    public void ExcludeAll()
    {
        foreach (var formatter in this.GetAllFormatters())
            formatter.ExcludeAll();
    }

    public void IncludeAll()
    {
        foreach (var formatter in this.GetAllFormatters())
            formatter.IncludeAll();
    }

    #region IFormattable

    public override string ToString() =>
        ToString(null, null);

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (formatProvider is not ILogState state)
            throw new NotImplementedException();
        var formatters = this.GetFormatters(includedOnly: true, prioritized: true);
        var sections = formatters.Select(formatter => formatter.ToString(format, state));
        return string.Join(Separator, sections);
    }

    #endregion

    #endregion
}