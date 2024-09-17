using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core
{
    class Authentication
    {
        public class Auth
        {
            public string Token { get; set; }
        }

        /// <summary>
        /// Sends a request to the WeatherCollector instance with the password (Really not the best way in my opinion) and hopes it's successful.
        /// </summary>
        /// <param name="pass">The password.</param>
        /// <param name="APILocation">The base location of the API.</param>
        /// <returns>Hopefully the authentication token, if not, probably an error.</returns>
        public static async Task<Auth> RequestAuth(string pass, string APILocation)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            client.BaseAddress = new Uri(APILocation, UriKind.Absolute);
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/login/?password=" + pass);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Auth>(await response.Content.ReadAsStringAsync());
        }
    }
}
