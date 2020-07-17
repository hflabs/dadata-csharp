using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public class OutwardClientAsync : ClientBaseAsync
    {
        protected const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        public OutwardClientAsync(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

        public async Task<SuggestResponse<T>> Suggest<T>(string query, int count = 5) where T : IOutward
        {
            var request = new SuggestOutwardRequest(query, count);
            return await Suggest<T>(request);
        }

        public async Task<SuggestResponse<T>> Suggest<T>(SuggestOutwardRequest request) where T : IOutward
        {
            var entity = Outwards.GetEntityName(typeof(T));
            return await Execute<SuggestResponse<T>>(method: SuggestionsMethod.Suggest, entity: entity, request: request);
        }

        public async Task<SuggestResponse<T>> Find<T>(string query) where T : IOutward
        {
            var request = new SuggestOutwardRequest(query);
            var entity = Outwards.GetEntityName(typeof(T));
            return await Execute<SuggestResponse<T>>(method: SuggestionsMethod.Find, entity: entity, request: request);
        }

        public async Task<SuggestResponse<T>> Geolocate<T>(double lat, double lon, int radius_meters = 100, int count = 5) where T : IOutward
        {
            var request = new GeolocateRequest(lat, lon, radius_meters, count);
            var entity = Outwards.GetEntityName(typeof(T));
            return await Execute<SuggestResponse<T>>(method: SuggestionsMethod.Geolocate, entity: entity, request: request);
        }
    }
}
