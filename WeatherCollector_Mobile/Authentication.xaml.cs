namespace WeatherCollector_Mobile;

public partial class Authentication : ContentPage
{
	public Authentication()
	{
		InitializeComponent();

        NavigationPage.SetHasBackButton(this, false);
        Shell.SetNavBarIsVisible(this, false);

        ServerMethod.Items.Add("https://");
        ServerMethod.Items.Add("http://");

        ServerMethod.SelectedIndex = 0;
    }

    protected override bool OnBackButtonPressed()
    {
        return false;
    }

    private async void LoginBtn_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ServerAddress.Text))
        {
            ServerAddress.Focus();
            return;
        }

        if (string.IsNullOrEmpty(Password.Text))
        {
            Password.Focus();
            return;
        }

        if (string.IsNullOrEmpty(Username.Text))
        {
            bool confirm = await DisplayAlert("Are you sure?", "The username field is blank, you may be using the built-in admin account instead of a user account, are you sure?", "Yes", "No");
            if(!confirm)
            {
                Username.Focus();
                return;
            }
        }

        string address = ServerAddress.Text;
        string username = Username.Text;
        string password = Password.Text;

        LoadingCover.IsVisible = true;

        Settings.currentSettings.RememberMe = RememberMe.IsChecked;
        Settings.SaveSettings();

        if(RememberMe.IsChecked)
        {
            Settings.currentSettings.URL = address;
            Settings.currentSettings.URLMethod = ServerMethod.SelectedIndex == 0 ? "https://" : "http://";
            Settings.currentSettings.Username = username;
            Settings.currentSettings.Password = password;

            Settings.SaveSettings();
        }

        try
        {
            Core.Authentication.Auth auth = await Core.Authentication.RequestAuth(Password.Text, (ServerMethod.SelectedIndex == 0 ? "https://" : "http://") + address);
            Settings.currentSettings.LastAuthenticationToken = auth.Token;
            Global.APILocation = address;
            Global.IsAuthenticated = true;
            Global.AuthenticationToken = auth.Token;

            Settings.SaveSettings();

            await Navigation.PopAsync();
        } catch
        {
            await DisplayAlert("Something went wrong.", "Failed to authenticate, check the details and try again.\n\nThings to check:\n" +
                "- Make sure that there is no \"http://\" or \"https://\" on the start of the URL.\n" +
                "- Make sure there is no \"/\" on the end of the URL.\n" +
                "- Make sure the username is right (If there is one)\n" +
                "- Make sure your password is correct", "Ok");
        }

        LoadingCover.IsVisible = false;
    }
}