using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public interface IOutwardClientAsync
    {
        Task<SuggestResponse<T>> Suggest<T>(string query, int count = 5) where T : IOutward;
        Task<SuggestResponse<T>> Suggest<T>(SuggestOutwardRequest request) where T : IOutward;
        Task<SuggestResponse<T>> Find<T>(string query) where T : IOutward;
        Task<SuggestResponse<T>> Geolocate<T>(double lat, double lon, int radius_meters = 100, int count = 5)
            where T : IOutward;
    }
}
