namespace WeatherCollector_Mobile;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private void AboutBtn_Clicked(object sender, EventArgs e)
    {
		About about = new About();
		Navigation.PushAsync(about);
    }
}