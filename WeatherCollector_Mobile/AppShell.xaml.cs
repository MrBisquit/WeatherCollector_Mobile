namespace WeatherCollector_Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("authentication", typeof(Authentication));

            if(Settings.FetchCurrentSettings().RememberMe)
            {
                Navigation.PushAsync(new LoadingPage());
            } else
            {
                Navigation.PushAsync(new Authentication());
            }

            ConnectionInfo.Text = $"Connected to: {Global.APILocation}";

            Core.Events.StatusChanged += StatusChanged;
            Core.Events.Error += Error;
        }

        public void StatusChanged(object? sender, EventArgs e)
        {
            ConnectionInfo.Text = $"Connected to: {Settings.FetchCurrentSettings().URL}\n" +
                $"Socket connected: {(Core.LiveUpdates.IsConnected ? "Yes" : "No")}\n" +
                $"API: {Settings.FetchCurrentSettings().UsedAPIVersion}\n" +
                $"App version: {AppInfo.Current.VersionString}\n" +
                $"App build: {AppInfo.Current.BuildString}";
        }

        public async void Error(object? sender, int error)
        {
            await DisplayAlert("An error occured", $"{Core.Errors.Descriptions[error].Value}\n\n(Error:{error})", "Ok");
        }

        private async void LogoutBtn_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Are you sure?", "You will be logged out and will have to sign back in.", "Logout", "Cancel");
            if(confirm)
            {
                Current.FlyoutIsPresented = false;
                await Current.GoToAsync("authentication");
            }
        }
    }
}
