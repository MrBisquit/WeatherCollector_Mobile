using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;

namespace WeatherCollector_Mobile.Core
{
    public static class LiveUpdates
    {
        public static Socket socket;
        public static bool IsConnected { get {  return socket != null && _IsConnected; } }
        private static bool _IsConnected = false;
        /// <summary>
        /// Attempts to connect to the socket connection (if there is one).
        /// </summary>
        /// <returns>If it succeeded or failed (Will return true pretty much all of the time because it's hard to tell).</returns>
        public static async Task<bool> InitialiseSocketConnection()
        {
            Debug.WriteLine("Attempting socket connection");
            Settings.SettingsObject settings = Settings.FetchCurrentSettings();

            Options options = new Options()
            {
                Query = new Dictionary<string, string> {
                    { "token", Global.AuthenticationToken }
                }
            };
            Manager manager = new Manager(new Uri(settings.URLMethod + settings.URL, UriKind.Absolute), options);
            socket = new Socket(manager , "");

            socket.On("error", () =>
            {
                Debug.WriteLine("Socket connection failed");
                Events.Error(null, 0);
                _IsConnected = false;
                Events.StatusChanged(null, EventArgs.Empty);
            });

            socket.Emit("ping", new[] { "ping" });

            socket.On("connect", () =>
            {
                Debug.WriteLine("Socket connected");
                _IsConnected = true;
                Events.StatusChanged(null, EventArgs.Empty);
            });

            socket.On("disconnect", () =>
            {
                Debug.WriteLine("Socket disconnected");
                Events.Error(null, 0);
                _IsConnected = false;
                Events.StatusChanged(null, EventArgs.Empty);
            });

            socket.On("reconnect_attempt", () =>
            {
                Debug.WriteLine("Attempting to reconnect socket connection");
            });

            socket.On("connect_error", () =>
            {
                Debug.WriteLine("Socket connection error");
                Events.Error(null, 0);
                _IsConnected = false;
                Events.StatusChanged(null, EventArgs.Empty);
            });

            socket.On("ping", () =>
            {
                Debug.WriteLine("Recieved ping");
                socket.Emit("pong", new string[0]);
                Debug.WriteLine("Sent pong");
                _IsConnected = true;
                Events.StatusChanged(null, EventArgs.Empty);
            });

            socket.On("update", () =>
            {
                Events.DataUpdate(null, EventArgs.Empty);
            });

            socket.On("image_update", () =>
            {
                Events.ImageUpdate(null, EventArgs.Empty);
            });

            Debug.WriteLine("Attempting socket connection...");
            socket.Connect();

            _IsConnected = true;
            Events.StatusChanged(null, EventArgs.Empty);

            await Task.Delay(10); // Give it change to attempt to connect before it continues

            return true;
        }
    }
}
