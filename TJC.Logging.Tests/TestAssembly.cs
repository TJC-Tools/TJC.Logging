namespace TJC.Logging.Tests;

[TestClass]
public sealed class TestAssembly
{
    [AssemblyInitialize]
    public static void Initialize(TestContext context) => MockMarkdownLogger.ResetLog();

    [AssemblyCleanup]
    public static void WriteMarkdownCalloutSamples()
    {
        TJC.Logging.Settings.Settings.ReloadDefaults();
        TJC.Logging.Settings.Settings.Instance.Formatting =
            new TJC.Logging.Settings.Format.FormatMarkdownSettings
            {
                TitleFormat = "{Message}",
                ContentFormat = "{Title}\n{Timestamp}\nException: {Exception}",
            };
        var logger = new MockMarkdownLogger();

        logger.LogMessage("Trace message", LogLevel.Trace);
        logger.LogMessage("Debug message", LogLevel.Debug);
        logger.LogMessage("Information message", LogLevel.Information);
        logger.LogMessage("Warning message", LogLevel.Warning);
        logger.LogMessage("Error message", LogLevel.Error);
        logger.LogMessage("Critical message", LogLevel.Critical);
        logger.LogMetadata(
            "Exception message",
            LogLevel.Error,
            exception: new Exception("Sample exception"),
            memberName: nameof(WriteMarkdownCalloutSamples)
        );
    }
}