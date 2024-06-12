namespace TJC.Logging.Tests.Extensions.Pluralize;

[TestClass]
public class PluralizeExtensionsTests
{
    [TestMethod]
    public void Pluralize_ObjectCollection_CountZero_ReturnsNonPluralized()
    {
        // Arrange
        var objects = new List<object>();

        // Act
        var result = objects.Pluralize("item");

        // Assert
        Assert.AreEqual("items", result);
    }

    [TestMethod]
    public void Pluralize_ObjectCollection_CountOne_ReturnsNonPluralized()
    {
        // Arrange
        var objects = new List<object> { new() };

        // Act
        var result = objects.Pluralize("item");

        // Assert
        Assert.AreEqual("item", result);
    }

    [DataRow(0, "items")]
    [DataRow(1, "item")]
    [DataRow(2, "items")]
    [DataTestMethod]
    public void Pluralize_Number_ReturnsCorrectPluralization(int number, string expected)
    {
        // Act
        var result = "item".Pluralize(number);

        // Assert
        Assert.AreEqual(expected, result, $"{number} {result}");
    }

    [DataRow(0, "items")]
    [DataRow(1, "item")]
    [DataRow(2, "items")]
    [DataTestMethod]
    public void Pluralize_Double_ReturnsCorrectPluralization(double number, string expected)
    {
        // Act
        var result = "item".Pluralize(number);

        // Assert
        Assert.AreEqual(expected, result, $"{number} {result}");
    }

    [TestMethod]
    public void IsOne_StringIsOne_ReturnsTrue()
    {
        // Arrange
        var number = "1";

        // Act
        var result = number.IsOne();

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsOne_StringIsNotOne_ReturnsFalse()
    {
        // Arrange
        var number = "2";

        // Act
        var result = number.IsOne();

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsOne_DoubleIsOne_ReturnsTrue()
    {
        // Arrange
        var number = 1.0;

        // Act
        var result = number.IsOne();

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsOne_DoubleIsNotOne_ReturnsFalse()
    {
        // Arrange
        var number = 2.0;

        // Act
        var result = number.IsOne();

        // Assert
        Assert.IsFalse(result);
    }
}