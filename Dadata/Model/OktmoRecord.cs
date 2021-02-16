namespace Dadata.Model
{
    public class OktmoRecord: IOutward
    {
        public string oktmo { get; set; }
        public string area_type { get; set; }
        public string area_code { get; set; }
        public string area { get; set; }
        public string subarea_type { get; set; }
        public string subarea_code { get; set; }
        public string subarea { get; set; }
    }
}
