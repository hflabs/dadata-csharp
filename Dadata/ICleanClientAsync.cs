using System.Collections.Generic;
using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public interface ICleanClientAsync
    {
        Task<T> Clean<T>(string source) where T : IDadataEntity;
        Task<IList<IDadataEntity>> Clean(IEnumerable<StructureType> structure, IEnumerable<string> data);
    }
}
