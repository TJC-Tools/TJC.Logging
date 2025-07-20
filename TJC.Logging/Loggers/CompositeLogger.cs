namespace TJC.Logging.Loggers;

internal class CompositeLogger(IEnumerable<ILogger> loggers) : ILogger
{
    private readonly IEnumerable<ILogger> _loggers = loggers;

    public IDisposable BeginScope<TState>(TState state) where TState : notnull =>
        NullScope.Instance; // Return a disposable that ends all scopes (if needed)

    public bool IsEnabled(LogLevel logLevel) => _loggers.Any(x => x.IsEnabled(logLevel));

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
                            Exception? exception, Func<TState, Exception?, string> formatter)
    {
        foreach (var logger in _loggers)
            if (logger.IsEnabled(logLevel))
                logger.Log(logLevel, eventId, state, exception, formatter);
    }

    private class NullScope : IDisposable
    {
        public static readonly NullScope Instance = new();
        public void Dispose() { }
    }
}
