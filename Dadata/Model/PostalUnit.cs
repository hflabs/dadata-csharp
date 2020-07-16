using System;
namespace Dadata.Model
{
    public class PostalUnit : IOutward
    {
        public string postal_code { get; set; }
        public bool is_closed { get; set; }
        public string type_code { get; set; }
        public string address_str { get; set; }
        public string address_kladr_id { get; set; }
        public string address_qc { get; set; }
        public double geo_lat { get; set; }
        public double geo_lon { get; set; }
        public string schedule_mon { get; set; }
        public string schedule_tue { get; set; }
        public string schedule_wed { get; set; }
        public string schedule_thu { get; set; }
        public string schedule_fri { get; set; }
        public string schedule_sat { get; set; }
        public string schedule_sun { get; set; }
    }
}
