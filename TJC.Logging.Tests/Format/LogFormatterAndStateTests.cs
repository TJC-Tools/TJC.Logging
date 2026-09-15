using TJC.Logging.Formatter;
using TJC.Logging.States;

namespace TJC.Logging.Tests.Format;

[TestClass]
public class LogFormatterAndStateTests
{
    [TestMethod]
    public void Formatter_AppendsMessageAndExceptionToState()
    {
        // Arrange
        var formatter = LogFormatter.Formatter<string>(" message");

        // Act
        var result = formatter("state", new Exception("failure"));
        var emptyStateResult = formatter(null!, null);

        // Assert
        Assert.AreEqual("state message | Exception: failure", result);
        Assert.AreEqual(" message", emptyStateResult);
    }

    [TestMethod]
    public void LogState_ExposesValuesAndFormatsItself()
    {
        // Arrange
        var state = new LogState(
            specialtyLogType: SpecialtyLogTypes.None,
            memberName: "Member",
            lineNumber: 7
        );

        // Act
        var formatted = state.ToString();

        // Assert
        Assert.AreEqual(SpecialtyLogTypes.None, state.Specialty);
        Assert.AreEqual("Member", state.MemberName);
        Assert.AreEqual(7, state.LineNumber);
        Assert.AreSame(state, state.GetFormat(null));
        Assert.IsNotNull(formatted);
    }
}