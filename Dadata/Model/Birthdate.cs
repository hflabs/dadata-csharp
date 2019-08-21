using System;
using Newtonsoft.Json;

namespace Dadata.Model
{
    public class Birthdate: IDadataEntity
    {
        public string source { get; set; }
        [JsonConverter(typeof(DateRuConverter))]
        public DateTime? birthdate { get; set; }
        public string qc { get; set; }

        public StructureType structure_type
        {
            get { return StructureType.BIRTHDATE; }
        }

        public override string ToString()
        {
            return string.Format("[BirthdateData: source={0}, birthdate={1}, qc={2}]", source, birthdate, qc);
        }
    }
}
