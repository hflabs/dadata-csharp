using System;
namespace Dadata.Model
{
    public class MetroStation : IOutward
    {
        public string city_kladr_id { get; set; }
        public string city_fias_id { get; set; }
        public string city { get; set; }
        public string name { get; set; }
        public string line_id { get; set; }
        public string line_name { get; set; }
        public double geo_lat { get; set; }
        public double geo_lon { get; set; }
        public string color { get; set; }
        public bool is_closed { get; set; }
    }
}
