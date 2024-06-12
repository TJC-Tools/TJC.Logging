namespace TJC.Logging.Extensions.Pluralize;

public static class PluralizeExtensions
{
    /// <summary>
    /// Pluralized a string based upon the number of objects within a collection
    /// </summary>
    /// <param name="objects">Collection to check if we require a pluralized output or not</param>
    /// <param name="nonPluralized">Non-Pluralized form of the string</param>
    /// <param name="pluralized">(Optional) Pluralized form of the string. If omitted, will return the Non-Pluralized form of the string with an 's' on the end</param>
    /// <returns></returns>
    public static string Pluralize(this IEnumerable<object> objects,
                                   string nonPluralized,
                                   string? pluralized = null) =>
        nonPluralized.Pluralize(objects.Count(), pluralized);

    /// <summary>
    /// Pluralized a string based upon the value of number
    /// </summary>
    /// <param name="nonPluralized">Non-Pluralized form of the string</param>
    /// <param name="number">Number (Pluralize unless equal to 1)</param>
    /// <param name="pluralized">(Optional) Pluralized form of the string. If omitted, will return the Non-Pluralized form of the string with an 's' on the end</param>
    /// <returns></returns>
    public static string Pluralize(this string nonPluralized,
                                   double number,
                                   string? pluralized = null) =>
        nonPluralized.Pluralize(!number.IsOne(), pluralized);

    /// <summary>
    /// Pluralized a string based upon the value of number
    /// </summary>
    /// <param name="nonPluralized">Non-Pluralized form of the string</param>
    /// <param name="number">Number (Pluralize unless equal to 1)</param>
    /// <param name="pluralized">(Optional) Pluralized form of the string. If omitted, will return the Non-Pluralized form of the string with an 's' on the end</param>
    /// <returns></returns>
    public static string Pluralize(this string nonPluralized,
                                   int number,
                                   string? pluralized = null) =>
        nonPluralized.Pluralize(!number.IsOne(), pluralized);

    /// <summary>
    /// Pluralized a string based upon a boolean
    /// </summary>
    /// <param name="nonPluralized">Non-Pluralized form of the string</param>
    /// <param name="pluralize">Should we pluralize or not</param>
    /// <param name="pluralized">(Optional) Pluralized form of the string. If omitted, will return the Non-Pluralized form of the string with an 's' on the end</param>
    /// <returns></returns>
    public static string Pluralize(this string nonPluralized,
                                   bool pluralize,
                                   string? pluralized = null) =>
        pluralize ? !string.IsNullOrEmpty(pluralized) ? pluralized :
            string.Concat(nonPluralized, 's') : nonPluralized;

    internal static bool IsOne(this string number) =>
        double.TryParse(number, out var value) && value.IsOne();

    internal static bool IsOne(this double number, double eps = double.Epsilon) =>
        Math.Abs(number - 1) < eps;

    internal static bool IsOne(this int number) =>
        number == 1;
}