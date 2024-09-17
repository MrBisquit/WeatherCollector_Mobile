using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core
{
    public static class Events
    {
        public static EventHandler StatusChanged;
        public static EventHandler SettingsUpdated;
        public static EventHandler DataRecieved;
        public static EventHandler ImageRecieved;
        public static EventHandler DataUpdate;
        public static EventHandler ImageUpdate;
        public static EventHandler<int> Error;
    }
}
