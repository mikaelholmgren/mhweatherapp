using mhweatherapp.Services;
using mhweatherapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhweatherapp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWeatherService _wc;
        private readonly IDBService _db;

        public IndexModel(IWeatherService wc, IDBService db)
        {
            
            this._wc = wc;
            this._db = db;
        }


        public Measurement Details { get; private set; }
        public CityStats Stats { get; private set; }

        public async Task OnGetAsync(string city, string date)
        {
            if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(date))
            {
                Details = await _db.GetMeasurementsForCityAndDateAsync(city, date);
            }
            else if (!string.IsNullOrEmpty(city))
            {
                Stats = await _db.GetCityStatsAsync(city);
            }

        }
    }
}
