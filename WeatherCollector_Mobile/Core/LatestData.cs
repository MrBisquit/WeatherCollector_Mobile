using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core
{
    public static class LatestData
    {
        public class Alert
        {
            [JsonProperty("type")]
            public int Type;

            [JsonProperty("level")]
            public int Level;

            [JsonProperty("title")]
            public string Title;

            [JsonProperty("description")]
            public string Description;
        }

        public class LatestDataObject
        {
            [JsonProperty("date")]
            public string Date; // Was DateTime but it complained

            [JsonProperty("temp")]
            public float Temp;

            [JsonProperty("pressure")]
            public float Pressure;

            [JsonProperty("light")]
            public float Light;

            [JsonProperty("humidity")]
            public float Humidity;

            [JsonProperty("wind_speed")]
            public float WindSpeed;

            [JsonProperty("wind_direction")]
            public float WindDir;

            [JsonProperty("rain")]
            public float Rain;

            [JsonProperty("rain_per_second")]
            public float RainPerSecond;

            [JsonProperty("alerts")]
            public Alert[] Alerts;
        }

        /// <summary>
        /// Fetches the latest collected data from the WeatherCollector instance.
        /// </summary>
        /// <param name="auth">The auth token.</param>
        /// <param name="APILocation">The base location of the API.</param>
        /// <returns>The latest data.</returns>
        public static async Task<LatestDataObject> RequestFull(string auth, string APILocation)
        {
            if (string.IsNullOrEmpty(auth)) return null;

            var client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            client.BaseAddress = new Uri(APILocation, UriKind.Absolute);
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/latest?format=CSJSON");
            request.Headers.Add("authorization", auth);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<LatestDataObject>(await response.Content.ReadAsStringAsync());
        }
    }
}
