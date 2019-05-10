using System;

namespace SNTN
{
    namespace Constants
    {
        internal static class Dates
        {
            public static DateTimeOffset StartDateTime => 
                new DateTime(
                    year: DateTime.Now.Year,
                    month: DateTime.Now.Month,
                    day: DateTime.Now.Day,
                    hour: 8,
                    minute: 0,
                    second: 0);

            public static DateTimeOffset StopDateTime => new 
                DateTime(
                    year: DateTime.Now.Year,
                    month: DateTime.Now.Month,
                    day: DateTime.Now.Day,
                    hour: 23,
                    minute: 59,
                    second: 59);

            public static DateTime CorrectMinimumDateTime
            {
                get
                {
                    if (DateTime.Now.Hour < 23)
                    {
                        return DateTime.Now;
                    }
                    else
                    {
                        return DateTime.Now.AddDays(1);
                    }
                }
            }
        }

        internal static class Strings
        {
            public static string Caption
            {
                get
                {
                    string _caption = string.Empty;
                    char[] _invisibleChars = { '\u2003', '\u2009', '\u200c', '\u200d', '\u1160' };
                    Random _random = new Random();
                    for (int i = 0; i < _random.Next(10, 20); ++i)
                    {
                        _caption += _invisibleChars[_random.Next(5)];
                    }
                    return _caption;
                }
            }
        }
    }
}
