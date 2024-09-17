namespace WeatherCollector_Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Core.Events.DataRecieved += DataRecieved;
            Core.Events.ImageRecieved += ImageRecieved;
        }

        private void DataRecieved(object? sender, EventArgs e)
        {
            UpdateView();
        }

        private void ImageRecieved(object? sender, EventArgs e)
        {
            UpdateView();
        }

        DateTime countDown = DateTime.Now;
        DateTime lastUpdated = DateTime.Now;
        IDispatcherTimer timer;
        WindDirectionDrawable windDirectionDrawable;

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            await Update();
            UpdateView();

            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += _timer_Tick;
            timer.Start();
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            UpdateCountDown();
        }

        private async Task Update()
        {
            Settings.UpdateGlobals();
            Reload.IsRefreshing = true;
            Core.LatestData.LatestDataObject latest = await Core.LatestData.RequestFull(Global.AuthenticationToken, Global.APILocation);
            if (latest == Global.latestData)
            {
                Reload.IsRefreshing = false;
                return;
            }

            Global.latestData = latest;
            Global.latestImage = await Core.LatestImage.RequestImage(Global.AuthenticationToken, Global.APILocation);
            return;
        }

        private void UpdateView()
        {
            Core.LatestData.LatestDataObject latest = Global.latestData;
            Temp.Text = $"{Math.Round(latest.Temp).ToString()}°C";
            Humidity.Text = $"{latest.Humidity.ToString()}%";
            Rain.Text = $"{latest.Rain.ToString()} mm ({latest.RainPerSecond * 60} mm/h)";
            WindDir.Text = $"{latest.WindDir.ToString()}°";
            WindSpeed.Text = $"{Math.Round(latest.WindSpeed * 2.23694, 2)} mph";
            Pressure.Text = $"{latest.Pressure} hPA";

            DateTime date = Core.Parsing.DateParser.JSTimeToDateTime(latest.Date);
            if (date.IsDaylightSavingTime())
            {
                date = date.AddHours(1);
            }
            lastUpdated = date;

            LastUpdated.Text = $"Last updated at: {Core.Parsing.DateParser.FormatDateTime(date)}";

            MemoryStream image = Global.latestImage;
            CurrentImage.Source = ImageSource.FromStream(() => image);
            CurrentImageLoader.IsRunning = false;

            Global.latestImage = image;
            Reload.IsRefreshing = false;

            windDirectionDrawable = new WindDirectionDrawable();
            WindDirectionView.Drawable = windDirectionDrawable;
            WindDirectionView.Invalidate();
        }

        private void UpdateCountDown()
        {
            countDown = lastUpdated.AddMinutes(5);
            TimeSpan countDownTo = countDown - DateTime.Now;
            if (countDownTo.Seconds <= 0 && countDownTo.Minutes <= 0 && countDownTo.Hours <= 0)
            {
                if (countDownTo.Seconds == 0 || countDownTo.Seconds % 15 == 0) Update(); // Old system, NEEDS SOCKET.IO INTEGRATION TO BE FINISHED
                PredictedUpdate.Text = "Predicted time until update: Waiting for update...";
                return;
            }
            PredictedUpdate.Text = $"Predicted time until update: {Core.Parsing.DateParser.FormatTimeSpan(countDownTo, true)}";
        }

        private async void RefreshBtn_Clicked(object sender, EventArgs e)
        {
            CurrentImageLoader.IsRunning = true;
            await Update();
        }

        private async void Reload_Refreshing(object sender, EventArgs e)
        {
            await Update();
            UpdateView();
        }

        public class WindDirectionDrawable : IDrawable
        {
            public void Draw(ICanvas canvas, RectF dirtRect)
            {
                AppTheme theme = Application.Current.RequestedTheme;
                double windAngle = Global.latestData.WindDir;

                float centerX = (float)dirtRect.Center.X;
                float centerY = (float)dirtRect.Center.Y;
                float radius = Math.Min(centerX, centerY) - 20;

                canvas.StrokeColor = theme == AppTheme.Dark ? Colors.LightGray : Colors.DarkGray;
                canvas.StrokeSize = 3;

                canvas.DrawLine((float)(centerX - 1.5), 35, (float)(centerX - 1.5), (radius * 2) + 5);
                canvas.DrawLine(35, (float)(centerY - 1.5), (radius * 2) + 5, (float)(centerY - 1.5));

                canvas.StrokeColor = theme == AppTheme.Dark ? Colors.White : Colors.Black;
                canvas.StrokeSize = 3;

                canvas.DrawCircle(centerX, centerY, radius - 15);

                canvas.FontColor = theme == AppTheme.Dark ? Colors.White : Colors.Black;
                canvas.FontSize = 20;
                /*canvas.DrawString("N", centerX - 10, centerY - radius - 10, HorizontalAlignment.Center);
                canvas.DrawString("E", centerX + radius + 10, centerY - 10, HorizontalAlignment.Center);
                canvas.DrawString("S", centerX - 10, centerY + radius + 10, HorizontalAlignment.Center);
                canvas.DrawString("W", centerX - radius - 30, centerY - 10, HorizontalAlignment.Center);*/
                canvas.DrawString("N", centerX, centerY - radius - 10, HorizontalAlignment.Center);
                canvas.DrawString("E", centerX + radius + 10, centerY + 5, HorizontalAlignment.Center);
                canvas.DrawString("S", centerX, centerY + radius + 20, HorizontalAlignment.Center);
                canvas.DrawString("W", centerX - radius - 10, centerY + 5, HorizontalAlignment.Center);

                canvas.StrokeColor = Colors.Red;
                canvas.StrokeSize = 5;

                double angleRadians = (Math.PI / 180) * windAngle;
                float arrowEndX = centerX + (float)((radius - 15) * Math.Sin(angleRadians));
                float arrowEndY = centerY - (float)((radius - 15) * Math.Cos(angleRadians));

                canvas.DrawLine(centerX, centerY, arrowEndX, arrowEndY);

                canvas.DrawLine(centerX, centerY, centerX - 15, centerY - 20);
                canvas.DrawLine(centerX, centerY, centerX - 15, centerY + 20);

                canvas.DrawLine(centerX - 15, centerY - 20, arrowEndX, arrowEndY);
                canvas.DrawLine(centerX - 15, centerY + 20, arrowEndX, arrowEndY);
            }
        }
    }
}
