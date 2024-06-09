namespace TJC.Logging.Tests.Mocks;

/// <summary>
/// Mock Trace Logger for Testing.
/// </summary>
internal class MockTraceLogger() : ILogger
{
    /// <summary>
    /// For Unit Testing that the message is formatted as expected.
    /// </summary>
    public string? LastMessage { get; private set; }

    /// <summary>
    /// Not implemented for Trace
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="state"></param>
    /// <returns></returns>
    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull =>
        null;

    /// <summary>
    /// Trace everything by default
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel logLevel) =>
        true;

    public void Log<TState>(LogLevel                         logLevel,
                            EventId                          eventId,
                            TState                           state,
                            Exception?                       exception,
                            Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var message = formatter(state, exception);
        if (string.IsNullOrEmpty(message))
            return;

        // Write to Trace with the appropriate log level
        switch (logLevel)
        {
            case LogLevel.Trace:
            case LogLevel.Debug:
                Trace.WriteLine(message);
                break;
            case LogLevel.Information:
                Trace.TraceInformation(message);
                break;
            case LogLevel.Warning:
                Trace.TraceWarning(message);
                break;
            case LogLevel.Error:
                Trace.TraceError(message);
                break;
            case LogLevel.Critical:
                Trace.Fail(message, exception?.ToString());
                break;
            case LogLevel.None:
                return;
            default:
                Trace.TraceInformation(message);
                break;
        }

        LastMessage = message;
    }
}