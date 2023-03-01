using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Utility
{
    public static class CurrentTime
    {
        public static DateTime DateTimeNow()
        {

            var dt = DateTime.UtcNow;
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Nepal Standard Time");
            var localTimeInNewYork = TimeZoneInfo.ConvertTimeFromUtc(dt, tz);
            return localTimeInNewYork;

        }
        public static string DateTimeToday()
        {


            var dt = DateTime.UtcNow;
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Nepal Standard Time");
            var localTimeInNewYork = TimeZoneInfo.ConvertTimeFromUtc(dt, tz);

            return localTimeInNewYork.Date.ToShortDateString();

        }

     
    }
}
