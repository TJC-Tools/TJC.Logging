namespace TJC.Logging.Tests.Extensions.ElapsedTime;

[TestClass]
public class GetElapsedTimeUnitsExtensionTests
{
    [DataRow(1, "d", false)]
    [DataRow(2, "d", false)]
    [DataRow(1, "day", true)]
    [DataRow(1.5, "days", true)]
    [DataRow(2, "days", true)]
    [DataRow(2.5, "days", true)]
    [DataTestMethod]
    public void GetElapsedTimeUnits_TotalDays_ReturnsCorrectUnits(double number,
                                                                  string expected,
                                                                  bool useLongName)
    {
        // Arrange
        var timeSpan = TimeSpan.FromDays(number);

        // Act
        var result = timeSpan.GetElapsedTimeUnits(useLongName: useLongName).Trim();

        // Assert
        Assert.AreEqual(expected, result, $"{number}{result}");
    }

    [DataRow(1, "h", false)]
    [DataRow(2, "h", false)]
    [DataRow(1, "hour", true)]
    [DataRow(1.5, "hours", true)]
    [DataRow(2, "hours", true)]
    [DataRow(2.5, "hours", true)]
    [DataTestMethod]
    public void GetElapsedTimeUnits_TotalHours_ReturnsCorrectUnits(double number,
                                                                   string expected,
                                                                   bool useLongName)
    {
        // Arrange
        var timeSpan = TimeSpan.FromHours(number);

        // Act
        var result = timeSpan.GetElapsedTimeUnits(useLongName: useLongName).Trim();

        // Assert
        Assert.AreEqual(expected, result, $"{number}{result}");
    }

    [DataRow(1, "m", false)]
    [DataRow(2, "m", false)]
    [DataRow(1, "minute", true)]
    [DataRow(1.5, "minutes", true)]
    [DataRow(2, "minutes", true)]
    [DataRow(2.5, "minutes", true)]
    [DataTestMethod]
    public void GetElapsedTimeUnits_TotalMinutes_ReturnsCorrectUnits(double number,
                                                                     string expected,
                                                                     bool useLongName)
    {
        // Arrange
        var timeSpan = TimeSpan.FromMinutes(number);

        // Act
        var result = timeSpan.GetElapsedTimeUnits(useLongName: useLongName).Trim();

        // Assert
        Assert.AreEqual(expected, result, $"{number}{result}");
    }

    [DataRow(1, "s", false)]
    [DataRow(2, "s", false)]
    [DataRow(1, "second", true)]
    [DataRow(1.5, "seconds", true)]
    [DataRow(2, "seconds", true)]
    [DataRow(2.5, "seconds", true)]
    [DataTestMethod]
    public void GetElapsedTimeUnits_TotalSeconds_ReturnsCorrectUnits(double number,
                                                                     string expected,
                                                                     bool useLongName)
    {
        // Arrange
        var timeSpan = TimeSpan.FromSeconds(number);

        // Act
        var result = timeSpan.GetElapsedTimeUnits(useLongName: useLongName).Trim();

        // Assert
        Assert.AreEqual(expected, result, $"{number}{result}");
    }

    [DataRow(1, "ms", false)]
    [DataRow(2, "ms", false)]
    [DataRow(1, "millisecond", true)]
    [DataRow(1.5, "milliseconds", true)]
    [DataRow(2, "milliseconds", true)]
    [DataRow(2.5, "milliseconds", true)]
    [DataTestMethod]
    public void GetElapsedTimeUnits_TotalMilliseconds_ReturnsCorrectUnits(double number,
                                                                          string expected,
                                                                          bool useLongName)
    {
        // Arrange
        var timeSpan = TimeSpan.FromMilliseconds(number);

        // Act
        var result = timeSpan.GetElapsedTimeUnits(useLongName: useLongName).Trim();

        // Assert
        Assert.AreEqual(expected, result, $"{number}{result}");
    }

    [DataRow(1, "μs", false)]
    [DataRow(2, "μs", false)]
    [DataRow(1, "microsecond", true)]
    [DataRow(1.5, "microseconds", true)]
    [DataRow(2, "microseconds", true)]
    [DataRow(2.5, "microseconds", true)]
    [DataTestMethod]
    public void GetElapsedTimeUnits_TotalMicroseconds_ReturnsCorrectUnits(double number,
                                                                          string expected,
                                                                          bool useLongName)
    {
        // Arrange
        var timeSpan = TimeSpan.FromMicroseconds(number);

        // Act
        var result = timeSpan.GetElapsedTimeUnits(useLongName: useLongName).Trim();

        // Assert
        Assert.AreEqual(expected, result, $"{number}{result}");
    }

    [DataRow(0, "ns", false)]
    [DataRow(1, "ns", false)]
    [DataRow(2, "ns", false)]
    [DataRow(0, "nanoseconds", true)]
    [DataRow(1, "nanoseconds", true)] // Note: 1 tick is 100 nanoseconds, so 1 nanosecond is not possible
    [DataRow(2, "nanoseconds", true)]
    [DataTestMethod]
    public void GetElapsedTimeUnits_TotalNanoseconds_ReturnsCorrectUnits(long ticks,
                                                                         string expected,
                                                                         bool useLongName)
    {
        // Arrange
        var timeSpan = TimeSpan.FromTicks(ticks / 1000);

        // Act
        var result = timeSpan.GetElapsedTimeUnits(useLongName: useLongName).Trim();

        // Assert
        Assert.AreEqual(expected, result, $"{ticks}{result}");
    }
}