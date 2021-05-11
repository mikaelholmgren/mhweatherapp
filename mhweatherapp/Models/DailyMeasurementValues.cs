using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhweatherapp.Models
{
    // These are for capturing historical daily weather values
    public class DailyMeasurementValues
    {
        public double RelativeHumidity { get; set; }
        public double AvgDailyTemp { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }


    }
}
