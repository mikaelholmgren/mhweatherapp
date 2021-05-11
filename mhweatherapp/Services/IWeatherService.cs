using mhweatherapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhweatherapp.Services
{
    public interface IWeatherService
    {
        Task<Measurement> GetCurrentWeatherAsync(string city);
        Task<Measurement> GetHistoricalWeatherAsync(string city, string date);
    }
}
