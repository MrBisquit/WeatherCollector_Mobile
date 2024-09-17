using System.Collections.ObjectModel;

namespace WeatherCollector_Mobile;

public partial class DataPage : ContentPage
{
	public DataPage()
	{
		InitializeComponent();
	}

    Core.V1.DataStucture.DataStuctureObject structure;
    bool LoadingData = false;

    public ObservableCollection<DataPart> data = new ObservableCollection<DataPart>();

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        try
        {
            structure = Global.Structure;
            if (structure == null) await FetchStructure();
            RefreshStructure();
        }
        catch { }
    }

    private void Reload_Refreshing(object sender, EventArgs e)
    {

    }

    private void RefreshStructure()
    {
        Year.Items.Clear();
        Month.Items.Clear();
        Day.Items.Clear();

        foreach (var year in structure.Years)
        {
            Year.Items.Add(year.Key);
        }

        Month.IsEnabled = false;
    }

    public async Task FetchStructure()
    {
        structure = await Core.APIParsing.FetchDataStructure(Global.AuthenticationToken, Global.APILocation, Core.APIParsing.APIVersion.V1);
        Global.Structure = structure;
    }

    private void Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Month.IsEnabled = true;
            Month.Items.Clear();

            foreach (var month in structure.Years[(string)Year.SelectedItem].Months)
            {
                Month.Items.Add(month.Key);
            }

            Day.IsEnabled = false;
            Day.Items.Clear();

            Month.SelectedIndex = 0;
        }
        catch { }
    }

    private void Month_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Month.SelectedIndex == 0) return;
            Day.IsEnabled = true;
            Day.Items.Clear();

            foreach (var day in structure.Years[(string)Year.SelectedItem].Months[(string)Month.SelectedItem].Days)
            {
                Day.Items.Add(day.Key);
            }

            Day.SelectedIndex = 0;
        }
        catch { }
    }

    private void Day_SelectedIndexChanged(object sender, EventArgs e)
    {
        FetchDataBtn.IsEnabled = true;
    }

    private async void FetchDataBtn_Clicked(object sender, EventArgs e)
    {
        LoadingData = true;
        Core.V1.FetchDay.DayObject day = await Core.V1.FetchDay.RequestDay(Global.AuthenticationToken, Global.APILocation, (string)Year.SelectedItem, (string)Month.SelectedItem, (string)Day.SelectedItem);

        data.Clear();

        for(int i = 0; i < day.DataPoints.Length; i++)
        {
            data.Add(new DataPart
            {
                ID = (i + 1).ToString(),
                DateSaved = Core.Parsing.DateParser.FormatDateTime(day.DataPoints[i].Date)
            });
        }

        Data.ItemsSource = data;
        Data.IsVisible = true;
        LoadingData = false;
    }
    
    public class DataPart
    {
        public string ID { get; set; } = string.Empty;
        public string DateSaved { get; set; } = string.Empty;
        public string Temp { get; set; } = string.Empty;
        public string Humiidty { get; set; } = string.Empty;
        public string Rain { get; set; } = string.Empty;
        public string RainPerHour {  get; set; } = string.Empty;
        public string WindDir { get; set; } = string.Empty;
        public string WindSpeed { get; set; } = string.Empty;
        public string Pressure { get; set; } = string.Empty;
        public string Light { get; set; } = string.Empty;
    }
}