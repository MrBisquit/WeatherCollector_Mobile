using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core
{
    public static class Errors
    {
        /// <summary>
        /// A static (readonly) list of error ids and their corresponding descriptions, all in one place so it's easy to change/repurpose later.
        /// </summary>
        public static readonly KeyValuePair<int, string>[] Descriptions = new KeyValuePair<int, string>[]
        {
            new(0, "Server socket connection failed")
        };
    }
}
