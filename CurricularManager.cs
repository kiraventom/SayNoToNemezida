using System;

namespace SNTN
{
    namespace Core
    {
        internal static class Curricular
        {
            public static (int h, int m)[] GetCurricular(DateTimeOffset? _nullableStartDateTime)
            {
                var _curricular = new System.Collections.Generic.Queue<(int h, int m)>();
                DateTimeOffset _startDateTime = (_nullableStartDateTime ?? Constants.Dates.StartDateTime);
                DateTimeOffset _postTime = DateTime.Now;
                Random _random = new Random();
                int _postsAmount = CalculatePostsAmount(_startDateTime);
                for (int i = 0; i < _postsAmount - 1; ++i)
                {
                    _postTime = _startDateTime.AddHours(i);
                    int _offset = _random.Next(5, 11);
                    _postTime = _postTime.AddMinutes(_offset);
                    _curricular.Enqueue((_postTime.Hour, _postTime.Minute));
                }
                // Last one is always on 23:59 to avoid bug with offset
                // Example: postTime = 23:59; offset = 3mins; 23:59 + 00:03 = 00:02 of the same day
                // TODO: fix 
                if (!_curricular.Contains((23, 59)))
                {
                    _curricular.Enqueue((23, 59));
                }
                return _curricular.ToArray();
            }

            private static int CalculatePostsAmount(DateTimeOffset _startDateTime)
            {
                return (Constants.Dates.StopDateTime.Hour + 1) - _startDateTime.Hour + 1;
            }
        }
    }
}
