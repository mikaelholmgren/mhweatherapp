using mhweatherapp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhweatherapp.Components
{
    public partial class CitiesNav
    {


        [Parameter]
        public string BasePage { get; set; }
        public IEnumerable<string> Cities { get; private set; }
        public string BaseUrl { get; private set; }
        protected override async Task OnInitializedAsync()
        {
            Cities = new HashSet<string>();
            
            Cities = await _db.GetCitiesAsync();

            BaseUrl = _lg.GetPathByPage(BasePage);
        }
    }
}
