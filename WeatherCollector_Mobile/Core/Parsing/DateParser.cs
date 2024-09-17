using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core.Parsing
{
    public static class DateParser
    {
        /// <summary>
        /// Converts a JS date string to a DateTime object.
        /// </summary>
        /// <param name="JSDate">The JS date string.</param>
        /// <returns>The generated DateTime object.</returns>
        public static DateTime JSTimeToDateTime(string JSDate)
        {
            Debug.WriteLine(JSDate);

            string[] split = JSDate.Split('T');
            string[] date = split[0].Split("-");
            string[] time = split[1].Split(".")[0].Split(':');

            int day = int.Parse(date[2]);
            int month = int.Parse(date[1]);
            int year = int.Parse(date[0]);

            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);
            int second = int.Parse(time[2]);

            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// Formats a DateTime object to a human-readable string.
        /// </summary>
        /// <param name="dt">The DateTime object.</param>
        /// <returns>The generated human-readable string.</returns>
        public static string FormatDateTime(DateTime dt)
        {
            return $"{dt.Day.ToString().PadLeft(2, '0')}/{dt.Month.ToString().PadLeft(2, '0')}/{dt.Year.ToString().PadLeft(4, '0')} at " +
                $"{dt.Hour.ToString().PadLeft(2, '0')}:{dt.Minute.ToString().PadLeft(2, '0')}:{dt.Second.ToString().PadLeft(2, '0')}";
        }

        /// <summary>
        /// Formats a DateTime object to a human-readable string.
        /// Only with the time.
        /// </summary>
        /// <param name="dt">The DateTime object.</param>
        /// <returns>The generated human-readable string.</returns>
        public static string FormatDateTime(DateTime dt, bool onlyTime)
        {
            return $"{dt.Hour.ToString().PadLeft(2, '0')}:{dt.Minute.ToString().PadLeft(2, '0')}:{dt.Second.ToString().PadLeft(2, '0')}";
        }

        /// <summary>
        /// Formats a TimeSpan object to a human-readable string.
        /// </summary>
        /// <param name="ts">The TimeSpan object.</param>
        /// <returns>The generated human-readable string.</returns>
        public static string FormatTimeSpan(TimeSpan ts)
        {
            return $"{ts.Days.ToString().PadLeft(2, '0')}:" +
                $"{ts.Hours.ToString().PadLeft(2, '0')}:{ts.Minutes.ToString().PadLeft(2, '0')}:{ts.Seconds.ToString().PadLeft(2, '0')}";
        }

        /// <summary>
        /// Formats a TimeSpan object to a human-readable string.
        /// Only with the time.
        /// </summary>
        /// <param name="ts">The TimeSpan object.</param>
        /// <returns>The generated human-readable string.</returns>
        public static string FormatTimeSpan(TimeSpan ts, bool onlyTime)
        {
            return $"{ts.Hours.ToString().PadLeft(2, '0')}:{ts.Minutes.ToString().PadLeft(2, '0')}:{ts.Seconds.ToString().PadLeft(2, '0')}";
        }
    }
}
