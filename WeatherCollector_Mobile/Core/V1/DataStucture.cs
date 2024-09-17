using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core.V1
{
    public static class DataStucture
    {
        /// <summary>
        /// Requests the full data structure for the WeatherCollector instance from the API V1.
        /// </summary>
        /// <param name="auth">The auth token.</param>
        /// <param name="APILocation">The base location of the API (protocol included).</param>
        /// <returns>The data structure returned from the server.</returns>
        public static async Task<DataStuctureObject> RequestFull(string auth, string APILocation)
        {
            if (string.IsNullOrEmpty(auth)) return null;

            var client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            client.BaseAddress = new Uri(APILocation, UriKind.Absolute);
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/full?format=CSJSON");
            request.Headers.Add("authorization", auth);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<DataStuctureObject>(await response.Content.ReadAsStringAsync());
        }

        public class DataStuctureObject
        {
            [JsonProperty("years")]
            public Dictionary<string, Year> Years { get; set; }
        }

        public class Year
        {
            [JsonProperty("months")]
            public Dictionary<string, Month> Months { get; set; }
        }

        public class Month
        {
            [JsonProperty("days")]
            public Dictionary<string, string> Days { get; set; }
        }
    }
}
