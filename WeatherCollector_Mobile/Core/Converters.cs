using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core
{
    public static class Converters
    {
        /// <summary>
        /// Converts m/s to mph.
        /// </summary>
        /// <param name="ms">M/s.</param>
        /// <returns>Mph.</returns>
        public static float MSToMPH(float ms)
        {
            return (float)(ms * 2.23694);
        }
    }
}
