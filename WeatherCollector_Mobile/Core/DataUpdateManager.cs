using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core
{
    public static class DataUpdateManager
    {
        /// <summary>
        /// Registers the event handlers.
        /// </summary>
        public static void Initialise()
        {
            Events.DataUpdate += DataUpdate;
            Events.ImageUpdate += ImageUpdate;
        }

        private static async void DataUpdate(object? sender, EventArgs e)
        {
            Global.latestData = await LatestData.RequestFull(Global.AuthenticationToken, Global.APILocation);
            Events.DataRecieved(sender, EventArgs.Empty);
        }

        private static async void ImageUpdate(object? sender, EventArgs e)
        {
            Global.latestImage = await LatestImage.RequestImage(Global.AuthenticationToken, Global.APILocation);
            Events.ImageRecieved(sender, EventArgs.Empty);
        }

        public static void RequestDataUpdate()
        {
            Task.Factory.StartNew(async () =>
            {
                LatestData.LatestDataObject data = await LatestData.RequestFull(Global.AuthenticationToken, Global.APILocation);
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Global.latestData = data;
                });
                Events.DataUpdate(null, EventArgs.Empty);
            });
        }

        public static void RequestImageUpdate()
        {
            Task.Factory.StartNew(async () =>
            {
                MemoryStream image = await LatestImage.RequestImage(Global.AuthenticationToken, Global.APILocation);
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Global.latestImage = image;
                });
                Events.ImageUpdate(null, EventArgs.Empty);
            });
        }
    }
}
