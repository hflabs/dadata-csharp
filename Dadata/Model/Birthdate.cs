using System;
namespace Dadata.Model
{
    public class Birthdate: IDadataEntity
    {
        public string source { get; set; }
        public string birthdate { get; set; }
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
