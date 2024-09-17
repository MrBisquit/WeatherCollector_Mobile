using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core
{
    public static class TakeImage
    {
        /// <summary>
        /// Requests the WeatherCollector instance to take a photo.
        /// This can take a while.
        /// DO NOT make this request shortly before it is due to update (It may not take the photo to be saved).
        /// </summary>
        /// <param name="auth">The auth token.</param>
        /// <param name="APILocation">The base location of the API.</param>
        /// <returns>The image as a MemoryStream.</returns>
        public static async Task<MemoryStream> RequestImage(string auth, string APILocation)
        {
            if (string.IsNullOrEmpty(auth)) return null;

            var client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            client.Timeout = new TimeSpan(0, 1, 0);
            client.BaseAddress = new Uri(APILocation, UriKind.Absolute);
            var request = new HttpRequestMessage(HttpMethod.Get, "/take_photo");
            request.Headers.Add("authorization", auth);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            byte[] imageData = await response.Content.ReadAsByteArrayAsync();
            return new MemoryStream(imageData);
        }
    }
}
