namespace TJC.Logging.Factories;

internal class LoggerFactory : ILoggerFactory
{
    private bool _disposed;

    private readonly List<ILoggerProvider> _providers = [];

    public void AddProvider(ILoggerProvider provider)
    {
        ObjectDisposedException.ThrowIf(_disposed, nameof(LoggerFactory));
        ArgumentNullException.ThrowIfNull(provider);
        _providers.Add(provider);
    }

    public ILogger CreateLogger(string categoryName)
    {
        ObjectDisposedException.ThrowIf(_disposed, nameof(LoggerFactory));
        var loggers = new List<ILogger>();
        foreach (var provider in _providers)
            loggers.Add(provider.CreateLogger(categoryName));
        return new CompositeLogger(loggers);
    }

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
