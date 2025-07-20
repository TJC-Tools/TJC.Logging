namespace TJC.Logging.Providers;

/// <summary>
/// Provider for <seealso cref="TraceLogger"/>.
/// </summary>
public class TraceLoggerProvider : ILoggerProvider
{
    /// <summary>
    /// Create <seealso cref="TraceLogger"/>.
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName) =>
        new TraceLogger(categoryName);

    /// <summary>
    /// Dispose.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
