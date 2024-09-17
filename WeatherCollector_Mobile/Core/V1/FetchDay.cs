using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core.V1
{
    public static class FetchDay
    {
        /// <summary>
        /// Requests a day of data from the WeatherCollector instance from the API V1.
        /// </summary>
        /// <param name="auth">The auth token.</param>
        /// <param name="APILocation">The base location of the API (protocol included).</param>
        /// <param name="year">The specified year.</param>
        /// <param name="month">The specified month.</param>
        /// <param name="day">The specified day.</param>
        /// <returns>The datapoints of that day.</returns>
        public static async Task<DayObject> RequestDay(string auth, string APILocation, string year, string month, string day)
        {
            if (string.IsNullOrEmpty(auth)) return null;

            var client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            client.BaseAddress = new Uri(APILocation, UriKind.Absolute);
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/data/{year}/{month}/{day}/");
            request.Headers.Add("authorization", auth);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<DayObject>(await response.Content.ReadAsStringAsync());
        }

        public class DayObject
        {
            [JsonProperty("points")]
            public DataPoint[] DataPoints;
        }

        public class DataPoint
        {
            [JsonProperty("temp")]
            public float Temp { get; set; }

            [JsonProperty("humidity")]
            public float Humidity { get; set; }

            [JsonProperty("pressure")]
            public float Pressure { get; set; }

            [JsonProperty("light")]
            public float Light { get; set; }

            [JsonProperty("wind_speed")]
            public float WindSpeed { get; set; }

            [JsonProperty("rain")]
            public float Rain { get; set; }

            [JsonProperty("rain_per_second")]
            public float RainPerSecond { get; set; }

            [JsonProperty("wind_direction")]
            public float WindDirection { get; set; }

            [JsonProperty("date")]
            public DateTime Date { get; set; }
        }
    }
}
