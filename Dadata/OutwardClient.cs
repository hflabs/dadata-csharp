using System;
using Dadata.Model;

namespace Dadata
{
    public class OutwardClient : ClientBase
    {
        const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        public OutwardClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

        public SuggestResponse<T> Suggest<T>(string query, int count = 5) where T : IOutward
        {
            var request = new SuggestOutwardRequest(query, count);
            return Suggest<T>(request);
        }

        public SuggestResponse<T> Suggest<T>(SuggestOutwardRequest request) where T : IOutward
        {
            var entity = Outwards.GetEntityName(typeof(T));
            return Execute<SuggestResponse<T>>(method: SuggestionsMethod.Suggest, entity: entity, request: request);
        }

        public SuggestResponse<T> Find<T>(string query) where T : IOutward
        {
            var request = new SuggestOutwardRequest(query);
            var entity = Outwards.GetEntityName(typeof(T));
            return Execute<SuggestResponse<T>>(method: SuggestionsMethod.Find, entity: entity, request: request);
        }

        public SuggestResponse<T> Geolocate<T>(double lat, double lon, int radius_meters = 100, int count = 5) where T : IOutward
        {
            var request = new GeolocateRequest(lat, lon, radius_meters, count);
            var entity = Outwards.GetEntityName(typeof(T));
            return Execute<SuggestResponse<T>>(method: SuggestionsMethod.Geolocate, entity: entity, request: request);
        }
    }
}
