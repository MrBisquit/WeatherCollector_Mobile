using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCollector_Mobile.Core.V1
{
    public static class Statistics
    {
        /**
         * [JsonProperty("temp")]
            public float Temp { get; set; }

            [JsonProperty("humidity")]
            public float Humidity { get; set; }

            [JsonProperty("pressure")]
            public float Pressure { get; set; }

            [JsonProperty("light")]
            public float Light { get; set; }

            [JsonProperty("wind_speed")]
            public float Wind_speed { get; set; }

            [JsonProperty("rain")]
            public float Rain { get; set; }

            [JsonProperty("rain_per_second")]
            public float RainPerSecond { get; set; }

            [JsonProperty("wind_direction")]
            public float WindDirection { get; set; }
        */

        public class DailyStatistics
        {
            public float AverageTemp;
            public float AverageHumidity;
            public float AveragePressure;
            public float AverageLight;
            public float AverageWindSpeed;
            public float TotalRain;
            public float AverageRainPerSecond;
            public float AverageRainPerHour;
            public float AverageWindDirection;

            public DataAndNamePair HighestTemp = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestTemp = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestHumidity = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestHumidity = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestPressure = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestPressure = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestLight = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestLight = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestWindSpeed = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestWindSpeed = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestRain = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestRain = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestRainPerSecond = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestRainPerSecond = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestRainPerHour = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestRainPerHour = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public List<DataAndNamePair> TemperatureGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> HumidityGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> RainGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> RainPerSecondGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> RainPerHourGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> WindSpeedGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> PressureGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> LightGraph = new List<DataAndNamePair>();
        }

        public class MonthlyStatistics
        {
            public float AverageTemp;
            public float AverageHumidity;
            public float AveragePressure;
            public float AverageLight;
            public float AverageWindSpeed;
            public float TotalRain;
            public float AverageRainPerSecond;
            public float AverageRainPerHour;
            public float AverageWindDirection;

            public DataAndNamePair HighestTemp = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestTemp = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestHumidity = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestHumidity = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestPressure = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestPressure = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestLight = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestLight = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestWindSpeed = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestWindSpeed = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestRain = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestRain = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestRainPerSecond = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestRainPerSecond = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public DataAndNamePair HighestRainPerHour = new() { Name = string.Empty, Value = float.NegativeInfinity };
            public DataAndNamePair LowestRainPerHour = new() { Name = string.Empty, Value = float.PositiveInfinity };

            public List<DataAndNamePair> TemperatureGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> HumidityGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> RainGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> RainPerSecondGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> RainPerHourGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> WindSpeedGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> WindDirectionGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> PressureGraph = new List<DataAndNamePair>();
            public List<DataAndNamePair> LightGraph = new List<DataAndNamePair>();
        }

        public class DataAndNamePair
        {
            public string Name { get; set; }
            public float Value { get; set; }
        }

        /// <summary>
        /// Fetches and processes a days worth of data and returns the statistical data for that day.
        /// </summary>
        /// <param name="year">The specified year.</param>
        /// <param name="month">The specified month.</param>
        /// <param name="day">The specified day.</param>
        /// <returns>The statistics.</returns>
        public static async Task<DailyStatistics> FetchDailyStatistics(string year, string month, string day)
        {
            FetchDay.DayObject dayObject = await FetchDay.RequestDay(Global.AuthenticationToken, Global.APILocation, year, month, day);
            int dps = dayObject.DataPoints.Length;

            DailyStatistics result = new DailyStatistics();
            foreach (var dp in dayObject.DataPoints)
            {
                result.AverageTemp += dp.Temp;
                result.AverageHumidity += dp.Humidity;
                result.AveragePressure += dp.Pressure;
                result.AverageLight += dp.Light;
                result.AverageWindSpeed += Converters.MSToMPH(dp.WindSpeed);
                result.TotalRain += dp.Rain;
                result.AverageRainPerSecond += dp.RainPerSecond;
                result.AverageRainPerHour += dp.RainPerSecond * 60;
                result.AverageWindDirection += dp.WindDirection;

                // Temp
                if(result.HighestTemp.Value < dp.Temp)
                {
                    result.HighestTemp.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.HighestTemp.Value = dp.Temp;
                }
                if(result.LowestTemp.Value > dp.Temp)
                {
                    result.LowestTemp.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.LowestTemp.Value = dp.Temp;
                }

                // Humidity
                if (result.HighestHumidity.Value < dp.Humidity)
                {
                    result.HighestHumidity.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.HighestHumidity.Value = dp.Humidity;
                }
                if (result.LowestHumidity.Value > dp.Humidity)
                {
                    result.LowestHumidity.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.LowestHumidity.Value = dp.Humidity;
                }

                // Pressure
                if (result.HighestPressure.Value < dp.Pressure)
                {
                    result.HighestPressure.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.HighestPressure.Value = dp.Pressure;
                }
                if (result.LowestPressure.Value > dp.Pressure)
                {
                    result.LowestPressure.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.LowestPressure.Value = dp.Pressure;
                }

                // Light
                if (result.HighestLight.Value < dp.Light)
                {
                    result.HighestLight.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.HighestLight.Value = dp.Light;
                }
                if (result.LowestLight.Value > dp.Light)
                {
                    result.LowestLight.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.LowestLight.Value = dp.Light;
                }

                // Wind Speed
                if (result.HighestWindSpeed.Value < dp.WindSpeed)
                {
                    result.HighestWindSpeed.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.HighestWindSpeed.Value = dp.WindSpeed;
                }
                if (result.LowestWindSpeed.Value > dp.WindSpeed)
                {
                    result.LowestWindSpeed.Name = Parsing.DateParser.FormatDateTime(dp.Date, true);
                    result.LowestWindSpeed.Value = dp.WindSpeed;
                }

                // Adding to the graphs
                result.TemperatureGraph.Add(new DataAndNamePair() { Name = Parsing.DateParser.FormatDateTime(dp.Date), Value = dp.Light });
                result.HumidityGraph.Add(new DataAndNamePair() { Name = Parsing.DateParser.FormatDateTime(dp.Date), Value = dp.Humidity });
                result.RainGraph.Add(new DataAndNamePair() { Name = Parsing.DateParser.FormatDateTime(dp.Date), Value = dp.Rain });
                result.RainPerSecondGraph.Add(new DataAndNamePair() { Name = Parsing.DateParser.FormatDateTime(dp.Date), Value = dp.RainPerSecond });
                result.RainPerHourGraph.Add(new DataAndNamePair() { Name = Parsing.DateParser.FormatDateTime(dp.Date), Value = dp.RainPerSecond * 60 });
                result.WindSpeedGraph.Add(new DataAndNamePair() { Name = Parsing.DateParser.FormatDateTime(dp.Date), Value = dp.WindSpeed });
                // Wind direction graph
                result.PressureGraph.Add(new DataAndNamePair() { Name = Parsing.DateParser.FormatDateTime(dp.Date), Value = dp.Pressure });
                result.LightGraph.Add(new DataAndNamePair() { Name = Parsing.DateParser.FormatDateTime(dp.Date), Value = dp.Light });
            }

            result.AverageTemp /= dps;
            result.AverageHumidity /= dps;
            result.AveragePressure /= dps;
            result.AverageLight /= dps;
            result.AverageWindSpeed /= dps;
            result.AverageRainPerSecond /= dps;
            result.AverageRainPerHour /= dps;
            result.AverageWindDirection /= dps;

            return result;
        }

        /// <summary>
        /// Fetches a months worth of data and processes it to return statistical data for the entire month.
        /// </summary>
        /// <param name="year">The specified year.</param>
        /// <param name="month">The specified month.</param>
        /// <returns>The statistics.</returns>
        public static async Task<MonthlyStatistics> FetchMonthlyStatistics(string year, string month)
        {
            Debug.WriteLine("Fetching monthly statistics...");
            Debug.WriteLine("Fetching data structure...");
            DataStucture.DataStuctureObject dataStructure = await APIParsing.FetchDataStructure(Global.AuthenticationToken, Global.APILocation, Core.APIParsing.APIVersion.V1);
            Debug.WriteLine("Done!");
            Debug.WriteLine($"Downloading {dataStructure.Years[year].Months[month].Days.Count} days data...");
            List<FetchDay.DayObject> days = new List<FetchDay.DayObject>();
            string[] keys = dataStructure.Years[year].Months[month].Days.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                Debug.WriteLine($"Downloading day {i + 1}...");
                //days.Add(await FetchDay.RequestDay(Global.AuthenticationToken, Global.APILocation, year, month, dataStructure.Years[year].Months[month].Days[keys[i]]));
                days.Add(await FetchDay.RequestDay(Global.AuthenticationToken, Global.APILocation, year, month, (i + 1).ToString()));
                Debug.WriteLine("Done!");
            }
            /*foreach (var day in dataStructure.Years[year].Months[month].Days)
            {
                Debug.WriteLine($"Downloading day {day.Key}");
                days.Add(await FetchDay.RequestDay(Global.AuthenticationToken, Global.APILocation, year, month, day.Value));
            }*/

            Debug.WriteLine("Analyzing information...");
            Debug.Indent();
            MonthlyStatistics result = new MonthlyStatistics();
            int dps = 0;
            for (int i = 0; i < days.Count; i++)
            {
                Debug.WriteLine($"Analyzing day No. {i + 1}");
                for (int j = 0; j < days[i].DataPoints.Length; j++)
                {
                    dps++;

                    FetchDay.DataPoint dp = days[i].DataPoints[j];

                    result.AverageTemp += dp.Temp;
                    result.AverageHumidity += dp.Humidity;
                    result.AveragePressure += dp.Pressure;
                    result.AverageLight += dp.Light;
                    result.AverageWindSpeed += Converters.MSToMPH(dp.WindSpeed);
                    result.TotalRain += dp.Rain;
                    result.AverageRainPerSecond += dp.RainPerSecond;
                    result.AverageRainPerHour += dp.RainPerSecond * 60;
                    result.AverageWindDirection += dp.WindDirection;

                    // Temp
                    if (result.HighestTemp.Value < dp.Temp)
                    {
                        result.HighestTemp.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.HighestTemp.Value = dp.Temp;
                    }
                    if (result.LowestTemp.Value > dp.Temp)
                    {
                        result.LowestTemp.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.LowestTemp.Value = dp.Temp;
                    }

                    // Humidity
                    if (result.HighestHumidity.Value < dp.Humidity)
                    {
                        result.HighestHumidity.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.HighestHumidity.Value = dp.Humidity;
                    }
                    if (result.LowestHumidity.Value > dp.Humidity)
                    {
                        result.LowestHumidity.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.LowestHumidity.Value = dp.Humidity;
                    }

                    // Pressure
                    if (result.HighestPressure.Value < dp.Pressure)
                    {
                        result.HighestPressure.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.HighestPressure.Value = dp.Pressure;
                    }
                    if (result.LowestPressure.Value > dp.Pressure)
                    {
                        result.LowestPressure.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.LowestPressure.Value = dp.Pressure;
                    }

                    // Light
                    if (result.HighestLight.Value < dp.Light)
                    {
                        result.HighestLight.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.HighestLight.Value = dp.Light;
                    }
                    if (result.LowestLight.Value > dp.Light)
                    {
                        result.LowestLight.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.LowestLight.Value = dp.Light;
                    }

                    // Wind Speed
                    if (result.HighestWindSpeed.Value < dp.WindSpeed)
                    {
                        result.HighestWindSpeed.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.HighestWindSpeed.Value = dp.WindSpeed;
                    }
                    if (result.LowestWindSpeed.Value > dp.WindSpeed)
                    {
                        result.LowestWindSpeed.Name = Parsing.DateParser.FormatDateTime(dp.Date);
                        result.LowestWindSpeed.Value = dp.WindSpeed;
                    }
                }
            }
            Debug.Unindent();
            Debug.WriteLine($"Analzyed {days.Count} days, in total there are {dps} datapoints, figuring out the averages...");
            result.AverageTemp /= dps;
            result.AverageHumidity /= dps;
            result.AveragePressure /= dps;
            result.AverageLight /= dps;
            result.AverageWindSpeed /= dps;
            result.AverageRainPerSecond /= dps;
            result.AverageRainPerHour /= dps;
            result.AverageWindDirection /= dps;

            Console.WriteLine("Averages:");
            Debug.Indent();
            Debug.WriteLine($"Temperature: {result.AverageTemp}°C");
            Debug.WriteLine($"Humidity:    {result.AverageHumidity}%");
            Debug.WriteLine($"Pressure:    {result.AveragePressure}hPA");
            Debug.WriteLine($"Light:       {result.AverageLight}lx");
            Debug.WriteLine($"Wind Speed:  {result.AverageWindSpeed}mph");
            Debug.WriteLine("Rain:");
            Debug.Indent();
            Debug.WriteLine($"Rain Per Second: {result.AverageRainPerSecond}mm/s");
            Debug.WriteLine($"Rain Per Hour:   {result.AverageRainPerHour}mm/h");
            Debug.WriteLine($"Total rain:      {result.TotalRain}mm");
            Debug.Unindent();
            Debug.WriteLine($"Wind Dir:    {result.AverageWindDirection}°");
            Debug.Unindent();
            Debug.WriteLine("Done!");
            return result;
        }
    }
}
