namespace TJC.Logging.Extensions.ElapsedTime;

public static class ElapsedTimeExtensions
{
    public static string GetElapsedTime(this TimeSpan timeSpan,
                                        int significantFigures = 2,
                                        bool useLongName = false,
                                        bool nanosecondsNoDecimals = true)
    {
        var value = timeSpan.GetElapsedTimeValue();
        var units = timeSpan.GetElapsedTimeUnits(out var isNanoseconds, useLongName);

        var formatSpecifier = nanosecondsNoDecimals && isNanoseconds ? string.Empty : $"F{significantFigures}";

        return string.Concat(value.ToString(formatSpecifier), units);
    }

    internal static double GetElapsedTimeValue(this TimeSpan timeSpan)
    {
        if (timeSpan.TotalDays >= 1)
            return timeSpan.TotalDays;
        else if (timeSpan.TotalHours >= 1)
            return timeSpan.TotalHours;
        else if (timeSpan.TotalMinutes >= 1)
            return timeSpan.TotalMinutes;
        else if (timeSpan.TotalSeconds >= 1)
            return timeSpan.TotalSeconds;
        else if (timeSpan.TotalMilliseconds >= 1)
            return timeSpan.TotalMilliseconds;
        else if (timeSpan.TotalMicroseconds >= 1)
            return timeSpan.TotalMicroseconds;
        else
            return timeSpan.TotalNanoseconds;
    }

    internal static string GetElapsedTimeUnits(this TimeSpan timeSpan,
                                              bool useLongName = false) =>
        timeSpan.GetElapsedTimeUnits(out _, useLongName);

    internal static string GetElapsedTimeUnits(this TimeSpan timeSpan,
                                              out bool isNanoseconds,
                                              bool useLongName = false)
    {
        isNanoseconds = false;
        string units;

        if (timeSpan.TotalDays >= 1)
            units = useLongName ? " day" : "d";
        else if (timeSpan.TotalHours >= 1)
            units = useLongName ? " hour" : "h";
        else if (timeSpan.TotalMinutes >= 1)
            units = useLongName ? " minute" : "m";
        else if (timeSpan.TotalSeconds >= 1)
            units = useLongName ? " second" : "s";
        else if (timeSpan.TotalMilliseconds >= 1)
            units = useLongName ? " millisecond" : "ms";
        else if (timeSpan.TotalMicroseconds >= 1)
            units = useLongName ? " microsecond" : "μs";
        else
        {
            units = useLongName ? " nanosecond" : "ns";
            isNanoseconds = true;
        }

        return useLongName ? units.Pluralize(timeSpan.GetElapsedTimeValue()) : units;
    }
}