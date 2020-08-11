using Dadata.Model;

namespace Dadata
{
    public interface IOutwardClientSync
    {
        SuggestResponse<T> Suggest<T>(string query, int count = 5) where T : IOutward;
        SuggestResponse<T> Suggest<T>(SuggestOutwardRequest request) where T : IOutward;
        SuggestResponse<T> Find<T>(string query) where T : IOutward;
        SuggestResponse<T> Geolocate<T>(double lat, double lon, int radius_meters = 100, int count = 5)
            where T : IOutward;
    }
}
