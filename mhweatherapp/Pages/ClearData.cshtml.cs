using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mhweatherapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mhweatherapp.Pages
{
    public class ClearDataModel : PageModel
    {
        private readonly IDBService db;

        public ClearDataModel(IDBService db)
        {
            this.db = db;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            // Uncomment to enable delete function
            await db.RemoveAllMeasurementsAsync();
            return RedirectToPage("/Index");
        }
    }
}
