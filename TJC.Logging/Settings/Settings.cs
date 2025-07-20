namespace TJC.Logging.Settings;

/// <summary>
/// Settings for logging.
/// </summary>
public sealed class Settings
{
    #region Fields

    private static Lazy<Settings> _instance = new();

    #endregion

    #region Properties

    /// <summary>
    /// Instance of logging settings.
    /// </summary>
    public static Settings Instance => _instance.Value;

    /// <summary>
    /// Formattings settings.
    /// </summary>
    public FormattingSettings Formatting { get; set; } = new();

    #endregion

    #region Methods

    /// <summary>
    /// Reload default settings for logging.
    /// </summary>
    public static void ReloadDefaults() => _instance = new Lazy<Settings>();

    #endregion
}
