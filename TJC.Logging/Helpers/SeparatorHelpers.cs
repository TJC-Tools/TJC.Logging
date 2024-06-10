namespace TJC.Logging.Helpers;

internal static class SeparatorHelpers
{
    internal static string AddSeparator(ref string input, string separator) =>
        input = input.EndsWith(separator) || string.IsNullOrEmpty(input) ? input : input + separator;

    internal static string AddSeparator(ref string input, string separator, string message) =>
        input = AddSeparator(ref input, separator) + message;

    internal static string AddSeparator(ref string input, string separator, object after) =>
        input = AddSeparator(ref input, separator, $"{after}");
}