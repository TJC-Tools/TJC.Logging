namespace TJC.Logging.Tests.Mocks;

/// <summary>
/// Mock logger that appends messages to a Markdown file for testing.
/// </summary>
internal sealed class MockMarkdownLogger : ILogger
{
    /// <summary>
    /// Markdown log file created by the tests.
    /// </summary>
    public static string LogFilePath { get; } = Path.Combine(
        Directory.GetCurrentDirectory(),
        "MarkdownLog.md"
    );

    /// <summary>
    /// Most recent message written to the file.
    /// </summary>
    public string? LastMessage { get; private set; }

    /// <summary>
    /// Deletes the previous test run's Markdown log.
    /// </summary>
    public static void ResetLog()
    {
        if (File.Exists(LogFilePath))
            File.Delete(LogFilePath);
    }

    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        if (!IsEnabled(logLevel))
            return;

        var message = formatter(state, exception);
        if (string.IsNullOrEmpty(message))
            return;

        File.AppendAllText(LogFilePath, message);
        LastMessage = message;
    }
}