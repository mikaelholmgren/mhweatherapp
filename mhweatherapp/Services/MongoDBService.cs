using mhweatherapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using mhweatherapp.Extensions;

namespace mhweatherapp.Services
{
    public class MongoDBService : IDBService
    {
        private readonly IConfiguration _config;
        private readonly MongoClient _mcl;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<Measurement> _weatherCollection;

        public MongoDBService(IConfiguration config)
        {
            this._config = config;
            _mcl = GetMongoClient();
            _db = _mcl.GetDatabase(_config["MongoDB:DBName"]);
            _weatherCollection = _db.GetCollection<Measurement>(_config["MongoDB:CollectionName"]);
        }

        private MongoClient GetMongoClient()
        {
            var connection = _config["MongoDB:ConnectionString"];
            MongoClient client = new MongoClient(connection);
            return client;
        }

        public async Task AddMeasurementAsync(Measurement m)
        {
            await _weatherCollection.InsertOneAsync(m);
        }

        public async Task<IEnumerable<Measurement>> GetAllMeasurementsAsync()
        {


            return await _weatherCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task RemoveAllMeasurementsAsync()
        {
            await _weatherCollection.DeleteManyAsync(new BsonDocument());
        }
        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            List<string> cities = new();
            var all = await GetAllMeasurementsAsync();
            foreach (var item in all)
            {
                if (!cities.Contains(item.City)) cities.Add(item.City);
            }
            return cities;
        }
        public async Task<IEnumerable<Measurement>> GetMeasurementsForCityAsync(string city)
        {
            var list = await _weatherCollection.Find(c => c.City == city).ToListAsync();
            return list;
        }

        public async Task<CityStats> GetCityStatsAsync(string city)
        {
            CityStats stats = null;
            var cityValues = await _weatherCollection.Find(c => c.City == city).ToListAsync();
            if (cityValues == null || !cityValues.Any()) return stats;
            cityValues = cityValues.OrderBy(d => d.Date).ToList();
            stats = new()
            {
                AverageTemp = Math.Round(cityValues.Average(v => v.DailyValues.AvgDailyTemp), 1),
                MinTemp = Math.Round(cityValues.Min(v => v.DailyValues.MinTemp), 1),
                MaxTemp = Math.Round(cityValues.Max(v => v.DailyValues.MaxTemp), 1),
                MeasurementCount = cityValues.Count(),
                CityName = city,
                SpringDate = FindSpring(cityValues),
                SummerDate = FindSummer(cityValues),
                Source = cityValues
            };
            return stats;
        }

        private DateTime? FindSummer(List<Measurement> cityValues)
        {
            DateTime? result = null, last = null;
            int validTempCount = 0;
            foreach (var date in cityValues)
            {
                var curDate = DateTime.Parse(date.Date);
                if (validTempCount == 5) break;
                if (Math.Round(date.DailyValues.AvgDailyTemp) > 10)
                {
                    validTempCount++;
                    
                }
                else
                {
                    validTempCount = 0;
                    result = null;
                    last = null;
                }
                if (validTempCount == 1) result = curDate; // maybe
                // check so that days are in a row, without gaps
                if (validTempCount > 1)
                {
                    if (!curDate.IsNextDay(last))
                    {
                        validTempCount = 0;
                        result = null;
                        last = null;

                    }
                }
                last = curDate;
            }
            if (validTempCount < 5) return null;
            return result;

        }

        private DateTime? FindSpring(List<Measurement> cityValues)
        {
            DateTime? result = null, last = null;
            int validTempCount = 0;
            foreach (var date in cityValues)
            {
                var curDate = DateTime.Parse(date.Date);
                if (validTempCount == 7) break;
                if (Math.Round(date.DailyValues.AvgDailyTemp) > 0)
                {
                    validTempCount++;
                    
                }
                else
                {
                    validTempCount = 0;
                    result = null;
                    last = null;
                }
                if (validTempCount == 1) result = curDate; // maybe
                // check so that days are in a row, without gaps
                if (validTempCount > 1)
                {
                    if (!curDate.IsNextDay(last))
                    {
                        validTempCount = 0;
                        result = null;
                        last = null;

                    }
                }
                last = curDate;
            }
            if (validTempCount < 7) return null;
            return result;
        }

        public async Task<Measurement> GetMeasurementsForCityAndDateAsync(string city, string date)
        {
            var item = await _weatherCollection.Find(c => c.City == city && c.Date == date).FirstOrDefaultAsync();
            return item;
        }

    }
}
