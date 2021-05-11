namespace mhweatherapp.Models
{
    // These are for the current weather values
    public class MeasurementValues
    {
        public double RelativeHumidity { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; }
        public int Clouds { get; set; }
        public float Uv { get; set; }
        public int Wind_dir { get; set; }
        public string Wind_dir_Text { get; set; }
        public double App_Temp { get; set; }

    }
}