using System;
namespace Dadata.Model
{
    /// <summary>
    /// Vehicle.
    /// </summary>
    public class Vehicle: IDadataEntity
    {
        public string source { get; set; }
        public string result { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string qc { get; set; }

        public StructureType structure_type
        {
            get { return StructureType.VEHICLE; }
        }

        public override string ToString()
        {
            return string.Format("[VehicleData: source={0}, result={1}, qc={2}]",
                source, result, qc);
        }
    }
}
