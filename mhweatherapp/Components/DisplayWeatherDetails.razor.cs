using mhweatherapp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhweatherapp.Components
{
    public partial class DisplayWeatherDetails
    {
        [Parameter]
        public Measurement Weather { get; set; }
    }
}
