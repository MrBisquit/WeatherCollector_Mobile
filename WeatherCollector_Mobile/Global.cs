using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile
{
    public static class Global
    {
        public static string AuthenticationToken = string.Empty;
        public static bool IsAuthenticated = false;
        public static string APILocation = "";
        public static Core.LatestData.LatestDataObject latestData;
        public static MemoryStream latestImage;

        public static Core.V1.DataStucture.DataStuctureObject Structure;
    }
}
