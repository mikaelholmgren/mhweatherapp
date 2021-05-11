using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhweatherapp.Models
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public MeasurementTypes Type { get; set; }
        public MeasurementValues CurrentValues { get; set; }
        public DailyMeasurementValues DailyValues { get; set; }
    }
}
