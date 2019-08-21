using System;
namespace Dadata.Model
{
    /// <summary>
    /// Passport.
    /// </summary>
    public class Passport: IDadataEntity
    {
        public string source { get; set; }
        public string series { get; set; }
        public string number { get; set; }
        public string qc { get; set; }

        public StructureType structure_type
        {
            get { return StructureType.PASSPORT; }
        }

        public override string ToString()
        {
            return string.Format("[PassportData: source={0}, series={1}, number={2}, qc={3}]",
                source, series, number, qc);
        }
    }
}
