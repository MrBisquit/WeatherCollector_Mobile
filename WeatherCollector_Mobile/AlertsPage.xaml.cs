namespace WeatherCollector_Mobile;

public partial class AlertsPage : ContentPage
{
	public AlertsPage()
	{
		InitializeComponent();
	}

    public static KeyValuePair<int, string>[] AlertColours = new KeyValuePair<int, string>[]
    {
        new KeyValuePair<int, string>(1, "22C55E"),
        new KeyValuePair<int, string>(2, "EAB308"),
        new KeyValuePair<int, string>(3, "F59E0B"),
        new KeyValuePair<int, string>(4, "EF4444")
    };

    public Grid CreateWeatherGrid(string title, string description, string fillColor)
    {
        Grid grid = new Grid();
        grid.BackgroundColor = Colors.Transparent;

        BoxView roundRectangle = new BoxView
        {
            Color = Color.FromArgb(fillColor),
            CornerRadius = 10,
            BackgroundColor = Colors.Transparent
        };

        StackLayout stackLayout = new StackLayout
        {
            Padding = new Thickness(5),
            BackgroundColor = Colors.Transparent
        };

        Label titleLabel = new Label
        {
            Text = title,
            FontAttributes = FontAttributes.Bold,
            FontSize = 20,
            LineBreakMode = LineBreakMode.WordWrap,
            TextColor = Colors.White
        };

        Label descriptionLabel = new Label
        {
            Text = description,
            LineBreakMode = LineBreakMode.WordWrap,
            TextColor = Colors.White
        };

        stackLayout.Children.Add(titleLabel);
        stackLayout.Children.Add(descriptionLabel);

        grid.Children.Add(roundRectangle);
        grid.Children.Add(stackLayout);

        return grid;
    }

    private async void Update()
    {
        Reload.IsRefreshing = true;

        AlertsList.Children.Clear();

        Settings.UpdateGlobals();
        Core.LatestData.LatestDataObject latest = await Core.LatestData.RequestFull(Global.AuthenticationToken, Global.APILocation);

        List<Core.LatestData.Alert> alerts = latest.Alerts.ToList().OrderBy(o => o.Level).ToList();
        for (int i = 0; i < alerts.Count; i++)
        {
            AlertsList.Children.Add(CreateWeatherGrid(alerts[i].Title, alerts[i].Description, AlertColours[alerts[i].Level].Value));
        }

        Reload.IsRefreshing = false;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Update();
    }

    private void Reload_Refreshing(object sender, EventArgs e)
    {
        Update();
    }
}