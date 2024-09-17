//using Android.Locations;

using System.Diagnostics;

namespace WeatherCollector_Mobile;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();

        NavigationPage.SetHasBackButton(this, false);
        Shell.SetNavBarIsVisible(this, false);
    }

    protected override bool OnBackButtonPressed()
    {
        return false;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        Debug.WriteLine("Authenticating...");
        Settings.SettingsObject settings = Settings.FetchCurrentSettings();
        try
        {
            Core.Authentication.Auth auth = await Core.Authentication.RequestAuth(settings.Password, settings.URLMethod + settings.URL);

            Core.Events.StatusChanged(this, EventArgs.Empty);

            Settings.currentSettings.LastAuthenticationToken = auth.Token;
            Global.IsAuthenticated = true;
            Global.AuthenticationToken = auth.Token;
            Settings.SaveSettings();

            try
            {
                Status.Text = "Fetching data structure...";
                Global.Structure = await Core.V1.DataStucture.RequestFull(Global.AuthenticationToken, Global.APILocation);
            }
            catch (Exception ex) { }

            Status.Text = "Establishing connection with server...";
            await Core.LiveUpdates.InitialiseSocketConnection();

            Status.Text = "Initialising data update manager...";
            Core.DataUpdateManager.Initialise();

            await Navigation.PopAsync();
        } catch(Exception ex)
        {
            Navigation.PushAsync(new Authentication());
            Navigation.RemovePage(this);
            await DisplayAlert("Something went wrong", "Authentication failed.\n\n" + ex.Message, "Ok");
        }
    }
}