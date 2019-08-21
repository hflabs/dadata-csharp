using System;
namespace Dadata.Model
{
    /// <summary>
    /// "As is" entity.
    /// </summary>
    public class AsIs: IDadataEntity
    {
        public string source { get; set; }

        public StructureType structure_type
        {
            get { return StructureType.AS_IS; }
        }

        public override string ToString()
        {
            return string.Format("[AsIsData: source={0}]", source);
        }
    }
}
