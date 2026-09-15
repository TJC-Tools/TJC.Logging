using TJC.Logging.Formatter;
using TJC.Logging.States;

namespace TJC.Logging.Tests.Format;


[Collection("Logging")]


public class LogFormatterAndStateTests
{
    [Fact]
    public void Formatter_AppendsMessageAndExceptionToState()
    {
        // Arrange
        var formatter = LogFormatter.Formatter<string>(" message");

        // Act
        var result = formatter("state", new Exception("failure"));
        var emptyStateResult = formatter(null!, null);

        // Assert
        Assert.Equal("state message | Exception: failure", result);
        Assert.Equal(" message", emptyStateResult);
    }

    [Fact]
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
        Assert.Equal(SpecialtyLogTypes.None, state.Specialty);
        Assert.Equal("Member", state.MemberName);
        Assert.Equal(7, state.LineNumber);
        Assert.Same(state, state.GetFormat(null));
        Assert.NotNull(formatted);
    }
}
