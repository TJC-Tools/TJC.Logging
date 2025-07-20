namespace TJC.Logging.Factories;

/// <summary>
/// Logger Factory.
/// </summary>
public class LoggerFactory : ILoggerFactory
{
    private bool _disposed;

    private readonly List<ILoggerProvider> _providers = [];

    /// <summary>
    /// Add <seealso cref="ILoggerProvider"/>'s to the system.
    /// </summary>
    /// <param name="provider"></param>
    public void AddProvider(ILoggerProvider provider)
    {
        ObjectDisposedException.ThrowIf(_disposed, nameof(LoggerFactory));
        ArgumentNullException.ThrowIfNull(provider);
        _providers.Add(provider);
    }

    /// <summary>
    /// Create a new <seealso cref="ILogger"/> instance.
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName)
    {
        ObjectDisposedException.ThrowIf(_disposed, nameof(LoggerFactory));
        var loggers = new List<ILogger>();
        foreach (var provider in _providers)
            loggers.Add(provider.CreateLogger(categoryName));
        return new CompositeLogger(loggers);
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        foreach (var provider in _providers)
            provider.Dispose();

        _disposed = true;

        GC.SuppressFinalize(this);
    }
}
