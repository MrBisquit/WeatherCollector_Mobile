using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace WeatherCollector_Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseSentry(options => {
                    // The DSN is the only required setting.
                    options.Dsn = "https://39cdf6f2c64a4afb79b2251ddb834aee@o4504395269799936.ingest.us.sentry.io/4507950720483328";

                    // Use debug mode if you want to see what the SDK is doing.
                    // Debug messages are written to stdout with Console.Writeline,
                    // and are viewable in your IDE's debug console or with 'adb logcat', etc.
                    // This option is not recommended when deploying your application.
                    // options.Debug = true; // ---------------------------------------------- Disabled for now

                    // Set TracesSampleRate to 1.0 to capture 100% of transactions for tracing.
                    // We recommend adjusting this value in production.
                    options.TracesSampleRate = 1.0;

                    // Other Sentry options can be set here.
                }).UseSkiaSharp(true);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
