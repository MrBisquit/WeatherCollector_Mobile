using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core
{
    public static class APIParsing
    {
        // This is literally just to make switching from one API version to another

        public enum APIVersion
        {
            V1 = 0
            // V2 is not released yet
        }

        /** Example:
         * public static (V1.DataStucture.DataStuctureObject?, V2.DataStructure.DataStuctureObject?) FetchDataStructure(string auth, string APILocation)
           {
                Switch statement here
           }
        **/

        public static async Task<V1.DataStucture.DataStuctureObject> FetchDataStructure(string auth, string APILocation, APIVersion _APIVersion)
        {
            return await V1.DataStucture.RequestFull(auth, APILocation);
        }

        public static async Task<V1.Statistics.DailyStatistics> FetchDailyStatistics(string auth, string APILocation, APIVersion _APIVersion, string year, string month, string day)
        {
            return await V1.Statistics.FetchDailyStatistics(year, month, day);
        }

        public static async Task<V1.Statistics.MonthlyStatistics> FetchMonthlyStatistics(string auth, string APILocation, APIVersion _APIVersion, string year, string month)
        {
            return await V1.Statistics.FetchMonthlyStatistics(year, month);
        }
    }
}
