namespace TJC.Logging.Tests.Mocks;

internal static class MockExceptionThrower
{
    public static void ThrowException(string message) => throw new Exception(message);

    public static void CallMethodToThrowException(string message) => ThrowException(message);

    public static void ThrowExceptionWithInnerException(string message, string innerMessage) =>
        throw new Exception(message, new Exception(innerMessage));

    public static void CallMethodToThrowExceptionWithInnerException(
        string message,
        string innerMessage
    ) => ThrowExceptionWithInnerException(message, innerMessage);
}
