using System;
using System.Collections.Generic;

namespace SNTN
{
    namespace Core
    {
        internal static class Curricular
        {
            public static (int h, int m)[] GetCurricular(DateTimeOffset? startDateTime/*, DateTimeOffset? stopDateTime*/)
            {
                var curricular = new Queue<(int h, int m)>();
                DateTimeOffset postTime = DateTime.Now;
                DateTimeOffset sdt = (startDateTime ?? Constants.Dates.StartDateTime);
                Random rnd = new Random();
                int postsAmount = CalculatePostsAmount(startDateTime);
                for (int i = 0; i < postsAmount; ++i)
                {
                    postTime = sdt.AddHours(i);
                    int offset = -5 + rnd.Next(11);
                    postTime = postTime.AddMinutes(offset);
                    curricular.Enqueue((postTime.Hour, postTime.Minute));
                }
                return curricular.ToArray();
            }

            private static int CalculatePostsAmount(DateTimeOffset? startDateTime)
            {
                DateTimeOffset sdt = (startDateTime ?? Constants.Dates.StartDateTime);
                return (Constants.Dates.StopDateTime.Hour + 1) - sdt.Hour + 1;
            }
        }
    }
}
