using System;
namespace Dadata.Model
{
    /// <summary>
    /// Fullname.
    /// </summary>
    public class Fullname: IDadataEntity
    {
        public string source { get; set; }
        public string result { get; set; }
        public string result_genitive { get; set; }
        public string result_dative { get; set; }
        public string result_ablative { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string gender { get; set; }
        public string qc { get; set; }

        public StructureType structure_type
        {
            get { return StructureType.NAME; }
        }

        public override string ToString()
        {
            return string.Format("[FioData: source={0}, surname={1}, name={2}, patronymic={3}, qc={4}]",
                source, surname, name, patronymic, qc);
        }
    }
}
