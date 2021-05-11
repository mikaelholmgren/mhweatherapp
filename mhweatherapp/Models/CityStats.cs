using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhweatherapp.Models
{
    public class CityStats
    {
        public double AverageTemp { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public int MeasurementCount { get; set; }
        public string CityName { get; set; }
        public DateTime? SpringDate { get; set; }
        public DateTime? SummerDate { get; set; }
        public IEnumerable<Measurement> Source { get; internal set; }
    }
}
