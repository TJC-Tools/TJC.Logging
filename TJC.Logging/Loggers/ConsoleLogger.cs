namespace TJC.Logging.Loggers;

/// <summary>
/// Simple logger for <see cref="Console"/>.
/// </summary>
/// <param name="categoryName"></param>
public class ConsoleLogger(string categoryName) : ILogger
{
    private readonly string _category = categoryName;

    /// <summary>
    /// Begins a logical operation scope.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="state"></param>
    /// <returns></returns>
    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
        => default!;

    /// <summary>
    /// Checks if the given <paramref name="logLevel"/> is enabled.
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel logLevel) => true;

    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="logLevel"></param>
    /// <param name="eventId"></param>
    /// <param name="state"></param>
    /// <param name="exception"></param>
    /// <param name="formatter"></param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine($"[{logLevel}] {_category}: {formatter(state, exception)}");
    }
}
