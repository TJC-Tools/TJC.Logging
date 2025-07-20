namespace TJC.Logging.Providers;

/// <summary>
/// Provider for <seealso cref="ConsoleLogger"/>.
/// </summary>
public class ConsoleLoggerProvider : ILoggerProvider
{
    /// <summary>
    /// Create <seealso cref="ConsoleLogger"/>.
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName) =>
        new ConsoleLogger(categoryName);

    /// <summary>
    /// Dispose.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
