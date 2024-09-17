using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile
{
    public static class Settings
    {
        public class SettingsObject
        {
            public bool RememberMe = false;
            public string URL = string.Empty;
            public string URLMethod = string.Empty;
            public string Username = string.Empty;
            public string Password = string.Empty;
            public string LastAuthenticationToken = string.Empty;
            public Core.APIParsing.APIVersion UsedAPIVersion = Core.APIParsing.APIVersion.V1;
        }

        public static SettingsObject? currentSettings = null;

        public static void SaveSettings()
        {
            if (currentSettings == null) return;

            File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, "settings.json"), JsonConvert.SerializeObject(currentSettings));
        }

        public static void LoadSettings()
        {
            if(File.Exists(Path.Combine(FileSystem.AppDataDirectory, "settings.json")))
            {
                currentSettings = JsonConvert.DeserializeObject<SettingsObject>(File.ReadAllText(Path.Combine(FileSystem.AppDataDirectory, "settings.json")));
            } else
            {
                currentSettings = new SettingsObject();
            }
        }

        public static SettingsObject FetchCurrentSettings()
        {
            if (currentSettings == null) LoadSettings();
            return currentSettings;
        }

        public static void UpdateGlobals()
        {
            SettingsObject settings = FetchCurrentSettings();
            Global.AuthenticationToken = settings.LastAuthenticationToken;
            Global.APILocation = settings.URLMethod + settings.URL;
        }
    }
}
