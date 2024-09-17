using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Maui;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace WeatherCollector_Mobile;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage()
    {
        InitializeComponent();
    }

    Core.V1.DataStucture.DataStuctureObject structure;
    bool LoadingData = false;

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        /*try
        {
            structure = await Core.APIParsing.FetchDataStructure(Global.AuthenticationToken, Global.APILocation, Core.APIParsing.APIVersion.V1);

            Year.Items.Clear();
            Month.Items.Clear();
            Day.Items.Clear();

            //Month.Items.Add("None");
            //Day.Items.Add("None");

            foreach (var year in structure.Years)
            {
                Year.Items.Add(year.Key);
            }

            Month.IsEnabled = false;
            Month.Items.Clear();
        } catch { }*/

        try
        {
            structure = Global.Structure;
            if (structure == null) await FetchStructure();
            RefreshStructure();
        } catch { }
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

            Month.Items.Add("None"); // For yearly statistics
            foreach (var month in structure.Years[(string)Year.SelectedItem].Months)
            {
                Month.Items.Add(month.Key);
            }

            Day.IsEnabled = false;
            Day.Items.Clear();

            Month.SelectedIndex = 0;
        } catch { }
    }

    private void Month_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FetchStatisticsBtn.IsEnabled = true;
            if (Month.SelectedIndex == 0) return;
            Day.IsEnabled = true;
            Day.Items.Clear();

            Day.Items.Add("None"); // For monthly statistics
            foreach (var day in structure.Years[(string)Year.SelectedItem].Months[(string)Month.SelectedItem].Days)
            {
                Day.Items.Add(day.Key);
            }

            Day.SelectedIndex = 0;
        } catch { }
    }

    private void Day_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private async void FetchStatisticsBtn_Clicked(object sender, EventArgs e)
    {
        LoadingData = true;
        Reload.IsRefreshing = true;

        if (Month.SelectedIndex == 0)
        {
            if (Settings.FetchCurrentSettings().UsedAPIVersion == Core.APIParsing.APIVersion.V1)
            {
                await DisplayAlert("Yearly statistics unavailable", "WeatherCollector API V1 doesn't support yearly statistics because of how statistics are done.", "Ok");
                Reload.IsRefreshing = false;
                return;
            }
        }
        else if (Day.SelectedIndex == 0)
        {
            Core.V1.Statistics.MonthlyStatistics statistics = await Core.APIParsing.FetchMonthlyStatistics(Global.AuthenticationToken, Global.APILocation,
                Settings.FetchCurrentSettings().UsedAPIVersion,
                (string)Year.SelectedItem, (string)Month.SelectedItem);

            AvgTemp.Text = $"{Math.Round(statistics.AverageTemp, 2)}°C";
            AvgHumidity.Text = $"{Math.Round(statistics.AverageHumidity, 2)}%";
            TotalRain.Text = $"{Math.Round(statistics.TotalRain, 2)} mm";
            AvgRPS.Text = $"{Math.Round(statistics.AverageRainPerSecond, 2)} mm/s";
            AvgRPH.Text = $"{Math.Round(statistics.AverageRainPerHour, 2)} mm/h";
            AvgWindDir.Text = $"{Math.Round(statistics.AverageWindDirection, 2)}°";
            AvgWindSpeed.Text = $"{Math.Round(statistics.AverageWindSpeed, 2)} mph";
            AvgPressure.Text = $"{Math.Round(statistics.AveragePressure, 2)} hPA";
            AvgLight.Text = $"{Math.Round(statistics.AverageLight, 2)} lx";

            HighTemp.Text = $"{Math.Round(statistics.HighestTemp.Value, 2)}°C ({statistics.HighestTemp.Name})";
            LowTemp.Text = $"{Math.Round(statistics.LowestTemp.Value, 2)}°C ({statistics.LowestTemp.Name})";

            HighHumidity.Text = $"{Math.Round(statistics.HighestHumidity.Value, 2)}% ({statistics.HighestHumidity.Name})";
            LowHumidity.Text = $"{Math.Round(statistics.LowestHumidity.Value, 2)}% ({statistics.LowestHumidity.Name})";

            HighWindSpeed.Text = $"{Math.Round(statistics.HighestWindSpeed.Value, 2)} mph ({statistics.HighestWindSpeed.Name})";
            LowWindSpeed.Text = $"{Math.Round(statistics.LowestWindSpeed.Value, 2)} mph ({statistics.LowestWindSpeed.Name})";

            HighPressure.Text = $"{Math.Round(statistics.HighestPressure.Value, 2)} hPA ({statistics.HighestPressure.Name})";
            LowPressure.Text = $"{Math.Round(statistics.LowestPressure.Value, 2)} hPA ({statistics.LowestPressure.Name})";

            HighLight.Text = $"{Math.Round(statistics.HighestLight.Value, 2)} lx ({statistics.HighestLight.Name})";
            LowLight.Text = $"{Math.Round(statistics.LowestLight.Value, 2)} lx ({statistics.LowestLight.Name})";

            DailyStatistics.IsVisible = true;
            Reload.IsRefreshing = false;
        }
        else if (Day.SelectedIndex != 0)
        {
            Core.V1.Statistics.DailyStatistics statistics = await Core.APIParsing.FetchDailyStatistics(Global.AuthenticationToken, Global.APILocation,
                Settings.FetchCurrentSettings().UsedAPIVersion,
                (string)Year.SelectedItem, (string)Month.SelectedItem, (string)Day.SelectedItem);

            AvgTemp.Text = $"{Math.Round(statistics.AverageTemp, 2)}°C";
            AvgHumidity.Text = $"{Math.Round(statistics.AverageHumidity, 2)}%";
            TotalRain.Text = $"{Math.Round(statistics.TotalRain, 2)} mm";
            AvgRPS.Text = $"{Math.Round(statistics.AverageRainPerSecond, 2)} mm/s";
            AvgRPH.Text = $"{Math.Round(statistics.AverageRainPerHour, 2)} mm/h";
            AvgWindDir.Text = $"{Math.Round(statistics.AverageWindDirection, 2)}°";
            AvgWindSpeed.Text = $"{Math.Round(statistics.AverageWindSpeed, 2)} mph";
            AvgPressure.Text = $"{Math.Round(statistics.AveragePressure, 2)} hPA";
            AvgLight.Text = $"{Math.Round(statistics.AverageLight, 2)} lx";

            HighTemp.Text = $"{Math.Round(statistics.HighestTemp.Value, 2)}°C ({statistics.HighestTemp.Name})";
            LowTemp.Text = $"{Math.Round(statistics.LowestTemp.Value, 2)}°C ({statistics.LowestTemp.Name})";

            HighHumidity.Text = $"{Math.Round(statistics.HighestHumidity.Value, 2)}% ({statistics.HighestHumidity.Name})";
            LowHumidity.Text = $"{Math.Round(statistics.LowestHumidity.Value, 2)}% ({statistics.LowestHumidity.Name})";

            HighWindSpeed.Text = $"{Math.Round(statistics.HighestWindSpeed.Value, 2)} mph ({statistics.HighestWindSpeed.Name})";
            LowWindSpeed.Text = $"{Math.Round(statistics.LowestWindSpeed.Value, 2)} mph ({statistics.LowestWindSpeed.Name})";

            HighPressure.Text = $"{Math.Round(statistics.HighestPressure.Value, 2)} hPA ({statistics.HighestPressure.Name})";
            LowPressure.Text = $"{Math.Round(statistics.LowestPressure.Value, 2)} hPA ({statistics.LowestPressure.Name})";

            HighLight.Text = $"{Math.Round(statistics.HighestLight.Value, 2)} lx ({statistics.HighestLight.Name})";
            LowLight.Text = $"{Math.Round(statistics.LowestLight.Value, 2)} lx ({statistics.LowestLight.Name})";

            // Temperature graph
            TempGraph.Title = new LabelVisual
            {
                Text = "Temperature (°C)",
                TextSize = 25,
                //Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

            List<double> _tempValues = new List<double>();
            List<string> _tempLabels = new List<string>();
            for (int i = 0; i < statistics.TemperatureGraph.Count; i++)
            {
                _tempValues.Add(statistics.TemperatureGraph[i].Value);
                _tempLabels.Add(statistics.TemperatureGraph[i].Name);
            }

            TempGraph.XAxes = new List<Axis>
            {
                new Axis
                {
                    Labels = _tempLabels,
                    LabelsRotation = 15,
                    TextSize = 10
                }
            };

            LineSeries<double> _tempSeries = new LineSeries<double>
            {
                Values = _tempValues,
                Fill = null
            };

            TempGraph.Series = new ISeries[] { _tempSeries };

            DailyStatistics.IsVisible = true;
            Reload.IsRefreshing = false;

            // Humidity graph
            HumidityGraph.Title = new LabelVisual
            {
                Text = "Humidity (%)",
                TextSize = 25,
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

            List<double> _humidityValues = new List<double>();
            List<string> _humidityLabels = new List<string>();
            for (int i = 0; i < statistics.HumidityGraph.Count; i++)
            {
                _humidityValues.Add(statistics.HumidityGraph[i].Value);
                _humidityLabels.Add(statistics.HumidityGraph[i].Name);
            }

            HumidityGraph.XAxes = new List<Axis>
            {
                new Axis
                {
                    Labels = _humidityLabels,
                    LabelsRotation = 15,
                    TextSize = 5
                }
            };

            LineSeries<double> _humiditySeries = new LineSeries<double>
            {
                Values = _humidityValues,
                Fill = null
            };

            HumidityGraph.Series = new ISeries[] { _humiditySeries };

            // --------------------------------------------------------------------------- The rest

            // Wind speed
            WindSpeedGraph.Title = new LabelVisual()
            {
                Text = "Wind Speed (mph)",
                TextSize = 25,
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

            List<double> _windSpeedValues = new List<double>();
            List<string> _windSpeedLabels = new List<string>();
            for (int i = 0; i < statistics.WindSpeedGraph.Count; i++)
            {
                _windSpeedValues.Add(statistics.WindSpeedGraph[i].Value);
                _windSpeedLabels.Add(statistics.WindSpeedGraph[i].Name);
            }

            WindSpeedGraph.XAxes = new List<Axis>
            {
                new Axis
                {
                    Labels = _windSpeedLabels,
                }
            };

            LineSeries<double> _windSpeedSeries = new LineSeries<double>
            {
                Values = _windSpeedValues,
                Fill = null
            };

            WindSpeedGraph.Series = new ISeries[] { _windSpeedSeries };

            // Wind direction + Wind Speed
            WindDirectionGraph.Title = new LabelVisual()
            {
                Text = "Wind direction (°) + Wind Speed (mph)",
                TextSize = 20,
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

            List<ObservablePolarPoint> _windDirectionValues = new List<ObservablePolarPoint>();
            for (int i = 0; i < statistics.WindSpeedGraph.Count; i++)
            {
                _windDirectionValues.Add(new ObservablePolarPoint(statistics.WindSpeedGraph[i].Value, statistics.WindSpeedGraph[i].Value));
            }

            WindDirectionGraph.AngleAxes = new PolarAxis[]
            {
                new PolarAxis
                {
                    MinLimit = 0,
                    MaxLimit = 360,
                    Labeler = angle => $"{angle}",
                    ForceStepToMin = true,
                    MinStep = 30,
                }
            };

            WindDirectionGraph.Series = new ISeries[]
            {
                new PolarLineSeries<ObservablePolarPoint>
                {
                    Values = _windDirectionValues.ToArray(),
                    IsClosed = true,
                    Fill = null
                }
            };
        }

        LoadingData = false;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        TempGraph.WidthRequest = width;
        HumidityGraph.WidthRequest = width;
        WindSpeedGraph.WidthRequest = width;
        WindDirectionGraph.WidthRequest = width;
    }

    private async void Reload_Refreshing(object sender, EventArgs e)
    {
        if (LoadingData) return;

        await FetchStructure();
        RefreshStructure();

        Reload.IsRefreshing = false;
    }
}