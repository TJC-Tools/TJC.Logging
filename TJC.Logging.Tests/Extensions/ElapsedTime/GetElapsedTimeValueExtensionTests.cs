namespace TJC.Logging.Tests.Extensions.ElapsedTime;

[TestClass]
public class GetElapsedTimeValueExtensionTests
{
    [TestMethod]
    public void GetElapsedTimeValue_TotalSeconds_ReturnsCorrectValue()
    {
        // Arrange
        var timeSpan = TimeSpan.FromSeconds(12);

        // Act
        var result = timeSpan.GetElapsedTimeValue();

        // Assert
        Assert.AreEqual(12, result);
    }

    [TestMethod]
    public void GetElapsedTimeValue_TotalMinutes_ReturnsCorrectValue()
    {
        // Arrange
        var timeSpan = TimeSpan.FromMinutes(5);

        // Act
        var result = timeSpan.GetElapsedTimeValue();

        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void GetElapsedTimeValue_TotalNanoseconds_ReturnsCorrectValue()
    {
        // Arrange
        var timeSpan = TimeSpan.FromTicks(42);

        // Act
        var result = timeSpan.GetElapsedTimeValue();

        // Assert
        Assert.AreEqual(4.2, result);
    }
}