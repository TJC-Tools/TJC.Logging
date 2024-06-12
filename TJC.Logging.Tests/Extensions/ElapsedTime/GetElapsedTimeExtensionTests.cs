namespace TJC.Logging.Tests.Extensions.ElapsedTime;

[TestClass]
public class GetElapsedTimeExtensionTests
{
    [TestMethod]
    public void GetElapsedTime_TotalMinutes_ReturnsCorrectElapsedTime()
    {
        // Arrange
        TimeSpan timeSpan = TimeSpan.FromMinutes(5);

        // Act
        string result = timeSpan.GetElapsedTime();

        // Assert
        Assert.AreEqual("5.00m", result);
    }

    [TestMethod]
    public void GetElapsedTime_TotalMinutes_LongName_ReturnsCorrectElapsedTime()
    {
        // Arrange
        TimeSpan timeSpan = TimeSpan.FromMinutes(5);

        // Act
        string result = timeSpan.GetElapsedTime(useLongName: true);

        // Assert
        Assert.AreEqual("5.00 minutes", result);
    }

    [TestMethod]
    public void GetElapsedTime_TotalMinutes_SigFigs0_LongName_ReturnsCorrectElapsedTime()
    {
        // Arrange
        TimeSpan timeSpan = TimeSpan.FromMinutes(5);

        // Act
        string result = timeSpan.GetElapsedTime(useLongName: true, significantFigures: 0);

        // Assert
        Assert.AreEqual("5 minutes", result);
    }
}