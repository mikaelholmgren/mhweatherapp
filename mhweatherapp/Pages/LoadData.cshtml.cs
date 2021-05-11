using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using mhweatherapp.Models;
using mhweatherapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mhweatherapp.Pages
{
    public class LoadDataModel : PageModel
    {
        private readonly IWeatherService _wc;
        private readonly IDBService _db;

        public LoadDataModel(IWeatherService wc, IDBService db)
        {
            this._wc = wc;
            this._db = db;

        }
        [BindProperty]
        public string CityName { get; set; }
        [BindProperty]
        public string Date { get; set; }
        public bool Exists { get; set; }
        public bool Confirm { get; set; }
        public Measurement CurrentWeather { get; set; }
        [TempData]
        public string CurrentWeatherJson { get; set; }
        public async Task<IActionResult> OnGetAsync(bool abort)
        {
            if (abort)
            {
                CurrentWeather = null;
                CurrentWeatherJson = "";
                return Page();
            }

            if (!string.IsNullOrEmpty(CurrentWeatherJson))
            {
                CurrentWeather = JsonSerializer.Deserialize<Measurement>(CurrentWeatherJson);
                await _db.AddMeasurementAsync(CurrentWeather);
                CurrentWeatherJson = "";
                ViewData["SavedData"] = "Data tillaggd till databasen.";
                CityName = "";
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(CityName)) return Page();
            Measurement current = null;
            if (!string.IsNullOrEmpty(Date))
            {
                current = await _db.GetMeasurementsForCityAndDateAsync(CityName, Date);
                Exists = current != null ? true : false;
                if (!Exists)
                    current = await _wc.GetHistoricalWeatherAsync(CityName, Date);
            }
            else
                current = await _wc.GetCurrentWeatherAsync(CityName);
            if (current == null) return Page();

            CurrentWeather = current;
            CurrentWeatherJson = JsonSerializer.Serialize(current);
            Confirm = true;


            return Page();
        }
    }
}
