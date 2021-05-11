using mhweatherapp.Models;
using mhweatherapp.Models.WeatherBit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace mhweatherapp.Services
{
    public class WeatherBitService : IWeatherService
    {
        private readonly HttpClient _hc;
        private readonly IConfiguration _config;
        private string _apiKey = "";
        public WeatherBitService(HttpClient hc, IConfiguration config)
        {
            this._hc = hc;
            this._config = config;
            _apiKey = _config["APIKeys:WeatherBitKey"];
        }
        public async Task<Measurement> GetCurrentWeatherAsync(string city)
        {

            string url = $"https://api.weatherbit.io/v2.0/current?city={city}&lang=sv&key={_apiKey}";
            var jsonString = await _hc.GetStringAsync(url);
            if (string.IsNullOrEmpty(jsonString)) return null;
            var weatherData = JsonSerializer.Deserialize<WeatherBitData>(jsonString);
            Measurement currentWeather = new()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.ToShortDateString(),
                City = city,
                CurrentValues = new()
                {
                    Temperature = Math.Round(weatherData.data[0].temp),
                    App_Temp = Math.Round((float)weatherData.data[0].app_temp),
                    WindSpeed = Math.Round(weatherData.data[0].wind_spd),
                    Wind_dir = weatherData.data[0].wind_dir,
                    Wind_dir_Text = weatherData.data[0].wind_cdir_full,
                    Uv = weatherData.data[0].uv,
                    Description = weatherData.data[0].weather.description,
                    RelativeHumidity = Math.Round(weatherData.data[0].rh),
                    Clouds = weatherData.data[0].clouds
                },
                Type = MeasurementTypes.Current
            };
            return currentWeather;
        }
        public async Task<Measurement> GetHistoricalWeatherAsync(string city, string date)
        {
            var startDate = DateTime.Parse(date);
            var endDate = startDate + TimeSpan.FromDays(1);
            string url = $"https://api.weatherbit.io/v2.0/history/daily?city={city}&lang=sv&start_date={startDate.ToShortDateString()}&end_date={endDate.ToShortDateString()}&key={_apiKey}";
            var jsonString = await _hc.GetStringAsync(url);
            if (string.IsNullOrEmpty(jsonString)) return null;
            var weatherData = JsonSerializer.Deserialize<WeatherBitData>(jsonString);
            Measurement currentWeather = new()
            {
                Id = Guid.NewGuid(),
                Date = startDate.ToShortDateString(),
                City = city,
                DailyValues = new()
                {
                    AvgDailyTemp = weatherData.data[0].temp,
                    MinTemp = weatherData.data[0].min_temp,
                    MaxTemp = weatherData.data[0].max_temp,
                    RelativeHumidity = Math.Round(weatherData.data[0].rh),
                },
                Type = MeasurementTypes.DailyHistorical
            };
            return currentWeather;
        }

    }
}
