namespace TJC.Logging.Settings;

public sealed class Settings
{
    #region Fields

    private static Lazy<Settings> _instance = new();

    #endregion

    #region Properties

    public static Settings Instance =>
        _instance.Value;

    public Formatting Formatting { get; set; } = new();

    #endregion

    #region Methods

    public static void ReloadDefaults() =>
        _instance = new Lazy<Settings>();

    #endregion
}