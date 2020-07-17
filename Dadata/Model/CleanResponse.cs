using System.Collections.Generic;

namespace Dadata.Model
{
    public class CleanResponse : IDadataResponse
    {
        public IList<StructureType> structure { get; set; }
        public IList<IList<IDadataEntity>> data { get; set; }
    }
}
