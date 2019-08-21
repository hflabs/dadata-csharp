using System;
namespace Dadata.Model
{
    /// <summary>
    /// Phone.
    /// </summary>
    public class Phone: IDadataEntity
    {
        public string source { get; set; }
        public string type { get; set; }
        public string phone { get; set; }
        public string country_code { get; set; }
        public string city_code { get; set; }
        public string number { get; set; }
        public string extension { get; set; }
        public string provider { get; set; }
        public string region { get; set; }
        public string timezone { get; set; }
        public string qc_conflict { get; set; }
        public string qc { get; set; }

        public StructureType structure_type
        {
            get { return StructureType.PHONE; }
        }

        public override string ToString()
        {
            return string.Format("[PhoneData: source={0}, phone={1}, qc={2}]",
                source, phone, qc);
        }
    }
}
