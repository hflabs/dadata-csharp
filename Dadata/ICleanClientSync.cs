using System.Collections.Generic;
using Dadata.Model;

namespace Dadata
{
    public interface ICleanClientSync
    {
        T Clean<T>(string source) where T : IDadataEntity;
        IList<IDadataEntity> Clean(IEnumerable<StructureType> structure, IEnumerable<string> data);
    }
}
