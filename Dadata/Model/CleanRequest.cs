using System;
using System.Collections.Generic;

namespace Dadata.Model
{
    /// <summary>
    /// Clean request.
    /// </summary>
    public class CleanRequest : IDadataRequest
    {
        public IEnumerable<StructureType> structure { get; }
        public IEnumerable<IEnumerable<string>> data { get; }

        public CleanRequest(IEnumerable<StructureType> structure, IEnumerable<string> data)
        {
            this.structure = structure;
            this.data = new List<IEnumerable<string>> { data };
        }

        public override string ToString()
        {
            return string.Format("[CleanQuery: structure={0}, data={1}]", string.Join(", ", structure), string.Join("\n", data));
        }
    }
}
