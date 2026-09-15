using TJC.Logging.Factories;
using TJC.Logging.Loggers;
using TJC.Logging.Providers;

namespace TJC.Logging.Tests;


[Collection("Logging")]


public class InfrastructureTests
{
    [Fact]
    public void Providers_CreateConcreteLoggersAndDispose()
    {
        // Arrange
        using var consoleProvider = new ConsoleLoggerProvider();
        using var traceProvider = new TraceLoggerProvider();

        // Act
        var consoleLogger = consoleProvider.CreateLogger("Console");
        var traceLogger = traceProvider.CreateLogger("Trace");

        // Assert
        Assert.IsType<ConsoleLogger>(consoleLogger);
        Assert.IsType<TraceLogger>(traceLogger);
    }

    [Fact]
    public void ConcreteLoggers_AcceptScopesAndWriteMessages()
    {
        // Arrange
        var consoleLogger = new ConsoleLogger("Console");
        var traceLogger = new TraceLogger("Trace");
        Func<string, Exception?, string> formatter = (state, exception) => state;

        // Act
        var consoleScope = consoleLogger.BeginScope("scope");
        var traceScope = traceLogger.BeginScope("scope");
        consoleLogger.Log(LogLevel.Information, default, "message", null, formatter);
        traceLogger.Log(LogLevel.Information, default, "message", null, formatter);

        // Assert
        Assert.Null(consoleScope);
        Assert.Null(traceScope);
        Assert.True(consoleLogger.IsEnabled(LogLevel.None));
        Assert.True(traceLogger.IsEnabled(LogLevel.None));
    }

    [Fact]
    public void CompositeLogger_LogsOnlyToEnabledLoggers()
    {
        // Arrange
        var enabledLogger = new RecordingLogger(true);
        var disabledLogger = new RecordingLogger(false);
        var logger = new CompositeLogger([enabledLogger, disabledLogger]);

        // Act
        using var scope = logger.BeginScope("scope");
        var isEnabled = logger.IsEnabled(LogLevel.Warning);
        logger.Log(LogLevel.Warning, default, "message", null, (state, exception) => state);

        // Assert
        Assert.True(isEnabled);
        Assert.Equal(1, enabledLogger.LogCount);
        Assert.Equal(0, disabledLogger.LogCount);
    }

    [Fact]
    public void LoggerFactory_ManagesProvidersAndRejectsUseAfterDisposal()
    {
        // Arrange
        var provider = new RecordingProvider();
        var factory = new LoggerFactory();

        // Act
        factory.AddProvider(provider);
        var logger = factory.CreateLogger("Category");
        var nullProviderException = Assert.Throws<ArgumentNullException>(() =>
            factory.AddProvider(null!)
        );
        factory.Dispose();
        factory.Dispose();

        // Assert
        Assert.NotNull(logger);
        Assert.Equal("Category", provider.CategoryName);
        Assert.Equal(1, provider.DisposeCount);
        Assert.Equal("provider", nullProviderException.ParamName);
        Assert.Throws<ObjectDisposedException>(() => factory.AddProvider(provider));
        Assert.Throws<ObjectDisposedException>(() => factory.CreateLogger("Category"));
    }

    private sealed class RecordingProvider : ILoggerProvider
    {
        public string? CategoryName { get; private set; }
        public int DisposeCount { get; private set; }

        public ILogger CreateLogger(string categoryName)
        {
            CategoryName = categoryName;
            return new RecordingLogger(true);
        }

        public void Dispose() => DisposeCount++;
    }

    private sealed class RecordingLogger(bool enabled) : ILogger
    {
        public int LogCount { get; private set; }

        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => enabled;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter
        ) => LogCount++;
    }
}
