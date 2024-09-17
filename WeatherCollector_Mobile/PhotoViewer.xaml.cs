namespace WeatherCollector_Mobile;

public partial class PhotoViewer : ContentPage
{
	public PhotoViewer()
	{
		InitializeComponent();
	}

    private async void Update()
    {
        CurrentImageLoader.IsRunning = true;

        MemoryStream image = await Core.TakeImage.RequestImage(Global.AuthenticationToken, Global.APILocation);
        CurrentImage.Source = ImageSource.FromStream(() => image);
        CurrentImageLoader.IsRunning = false;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Update();
    }

    private void TakePhotoBtn_Clicked(object sender, EventArgs e)
    {
        Update();
    }
}