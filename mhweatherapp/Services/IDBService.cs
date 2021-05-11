using mhweatherapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhweatherapp.Services
{
    public interface IDBService
    {
        Task AddMeasurementAsync(Measurement m);
        Task<IEnumerable<Measurement>> GetAllMeasurementsAsync();
        Task RemoveAllMeasurementsAsync();
        Task<IEnumerable<string>> GetCitiesAsync();
        Task<IEnumerable<Measurement>> GetMeasurementsForCityAsync(string city);
        Task<CityStats> GetCityStatsAsync(string city);
        Task<Measurement> GetMeasurementsForCityAndDateAsync(string city, string date);
    }
}
