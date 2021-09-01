using System.Threading;
using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public interface IOutwardClientAsync
    {
        Task<SuggestResponse<T>> Suggest<T>(string query, int count = 5, CancellationToken cancellationToken = default)
            where T : IOutward;

        Task<SuggestResponse<T>> Suggest<T>(SuggestOutwardRequest request,
            CancellationToken cancellationToken = default) where T : IOutward;

        Task<SuggestResponse<T>> Find<T>(string query, CancellationToken cancellationToken = default)
            where T : IOutward;

        Task<SuggestResponse<T>> Geolocate<T>(double lat, double lon, int radius_meters = 100, int count = 5,
            CancellationToken cancellationToken = default)
            where T : IOutward;
    }
}
