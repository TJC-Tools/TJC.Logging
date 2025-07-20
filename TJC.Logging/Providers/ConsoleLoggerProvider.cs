namespace TJC.Logging.Providers;

internal class ConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) =>
        new ConsoleLogger(categoryName);

    public void Dispose() { }
}
