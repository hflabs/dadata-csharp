using System.Collections.Specialized;
using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public class SuggestClientAsync : ClientBaseAsync
    {
        protected const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        public SuggestClientAsync(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

        #region Address

        public async Task<SuggestResponse<Address>> SuggestAddress(string query, int count = 5)
        {
            var request = new SuggestAddressRequest(query, count);
            return await SuggestAddress(request);
        }

        public async Task<SuggestResponse<Address>> SuggestAddress(SuggestAddressRequest request)
        {
            return await Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Address, request: request);
        }

        public async Task<SuggestResponse<Address>> FindAddress(string query)
        {
            var request = new FindAddressRequest(query);
            return await FindAddress(request);
        }

        public async Task<SuggestResponse<Address>> FindAddress(FindAddressRequest request)
        {
            return await Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Address, request: request);
        }

        public async Task<SuggestResponse<Address>> Geolocate(double lat, double lon, int radius_meters = 100, int count = 5)
        {
            var request = new GeolocateRequest(lat, lon, radius_meters, count);
            return await Geolocate(request);
        }

        public async Task<SuggestResponse<Address>> Geolocate(GeolocateRequest request)
        {
            return await Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Geolocate, entity: SuggestionsEntity.Address, request: request);
        }

        public async Task<IplocateResponse> Iplocate(string ip, string language = "ru")
        {
            var parameters = new NameValueCollection();
            parameters.Add("ip", ip);
            parameters.Add("language", language);
            return await ExecuteGet<IplocateResponse>(method: SuggestionsMethod.Iplocate, entity: SuggestionsEntity.Address, parameters: parameters);
        }

        #endregion

        #region Bank

        public async Task<SuggestResponse<Bank>> SuggestBank(string query, int count = 5)
        {
            var request = new SuggestBankRequest(query, count);
            return await SuggestBank(request);
        }

        public async Task<SuggestResponse<Bank>> SuggestBank(SuggestBankRequest request)
        {
            return await Execute<SuggestResponse<Bank>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Bank, request: request);
        }

        public async Task<SuggestResponse<Bank>> FindBank(string query)
        {
            var request = new FindBankRequest(query);
            return await FindBank(request);
        }

        public async Task<SuggestResponse<Bank>> FindBank(FindBankRequest request)
        {
            return await Execute<SuggestResponse<Bank>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Bank, request: request);
        }

        #endregion

        #region Email

        public async Task<SuggestResponse<Email>> SuggestEmail(string query, int count = 5)
        {
            var request = new SuggestRequest(query, count);
            return await SuggestEmail(request);
        }

        public async Task<SuggestResponse<Email>> SuggestEmail(SuggestRequest request)
        {
            return await Execute<SuggestResponse<Email>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Email, request: request);
        }

        #endregion

        #region Fias

        public async Task<SuggestResponse<FiasAddress>> SuggestFias(string query, int count = 5)
        {
            var request = new SuggestAddressRequest(query, count);
            return await SuggestFias(request);
        }

        public async Task<SuggestResponse<FiasAddress>> SuggestFias(SuggestAddressRequest request)
        {
            return await Execute<SuggestResponse<FiasAddress>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Fias, request: request);
        }

        public async Task<SuggestResponse<Address>> FindFias(string query)
        {
            var request = new SuggestRequest(query);
            return await FindFias(request);
        }

        public async Task<SuggestResponse<Address>> FindFias(SuggestRequest request)
        {
            return await Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Fias, request: request);
        }

        #endregion

        #region Name

        public async Task<SuggestResponse<Fullname>> SuggestName(string query, int count = 5)
        {
            var request = new SuggestNameRequest(query, count);
            return await SuggestName(request);
        }

        public async Task<SuggestResponse<Fullname>> SuggestName(SuggestNameRequest request)
        {
            return await Execute<SuggestResponse<Fullname>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Name, request: request);
        }

        #endregion

        #region Party

        public async Task<SuggestResponse<Party>> SuggestParty(string query, int count = 5)
        {
            var request = new SuggestPartyRequest(query, count);
            return await SuggestParty(request);
        }

        public async Task<SuggestResponse<Party>> SuggestParty(SuggestPartyRequest request)
        {
            return await Execute<SuggestResponse<Party>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Party, request: request);
        }

        public async Task<SuggestResponse<Party>> FindParty(string query)
        {
            var request = new FindPartyRequest(query);
            return await FindParty(request);
        }

        public async Task<SuggestResponse<Party>> FindParty(FindPartyRequest request)
        {
            return await Execute<SuggestResponse<Party>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Party, request: request);
        }

        public async Task<SuggestResponse<Party>> FindAffiliated(string query)
        {
            var request = new FindAffiliatedRequest(query);
            return await FindAffiliated(request);
        }

        public async Task<SuggestResponse<Party>> FindAffiliated(FindAffiliatedRequest request)
        {
            return await Execute<SuggestResponse<Party>>(method: SuggestionsMethod.FindAffiliated, entity: SuggestionsEntity.Party, request: request);
        }

        #endregion
    }
}
