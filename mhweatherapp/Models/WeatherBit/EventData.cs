namespace mhweatherapp.Models.WeatherBit
{
    public class EventData
    {
        public double rh { get; set; }
        public int clouds { get; set; }
        public string city_name { get; set; }
        public float wind_spd { get; set; }
        public string wind_cdir_full { get; set; }
        public string sunset { get; set; }
        public float uv { get; set; }
        public int wind_dir { get; set; }
        public string sunrise { get; set; }
        public Weather weather { get; set; }
        public string datetime { get; set; }
        public double temp { get; set; }
        public double app_temp { get; set; }
        public double min_temp { get; set; }
        public double max_temp { get; set; }
    }

}

