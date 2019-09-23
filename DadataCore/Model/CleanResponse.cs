using System;
using System.Collections.Generic;

namespace DadataCore.Model
{
    /// <summary>
    /// Clean response.
    /// </summary>
    public class CleanResponse : IDadataResponse
    {
        public IList<StructureType> structure { get; set; }
        public IList<IList<IDadataEntity>> data { get; set; }
    }
}
