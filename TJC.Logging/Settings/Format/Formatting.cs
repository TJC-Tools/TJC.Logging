namespace TJC.Logging.Settings.Format;

public class Formatting
{
    #region Properties

    public FormatLocation Location { get; set; } = new();

    #endregion

    #region Methods

    public void ExcludeAll()
    {
        foreach (var formatter in this.GetFormatters())
            formatter.ExcludeAll();
    }

    public void IncludeAll()
    {
        foreach (var formatter in this.GetFormatters())
            formatter.IncludeAll();
    }

    #endregion
}