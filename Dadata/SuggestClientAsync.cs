using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public class SuggestClientAsync : ClientBaseAsync, ISuggestClientAsync
    {
        protected const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        public SuggestClientAsync(string token, string baseUrl = BASE_URL, HttpClient client = null)
            : base(token, baseUrl, client) { }

        #region Address

        public async Task<SuggestResponse<Address>> SuggestAddress(string query, int count = 5,
            CancellationToken cancellationToken = default)
        {
            var request = new SuggestAddressRequest(query, count);
            return await SuggestAddress(request);
        }

        public async Task<SuggestResponse<Address>> SuggestAddress(SuggestAddressRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Suggest,
                entity: SuggestionsEntity.Address, request: request, cancellationToken: cancellationToken);
        }

        public async Task<SuggestResponse<Address>> FindAddress(string query,
            CancellationToken cancellationToken = default)
        {
            var request = new FindAddressRequest(query);
            return await FindAddress(request, cancellationToken);
        }

        public async Task<SuggestResponse<Address>> FindAddress(FindAddressRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Find,
                entity: SuggestionsEntity.Address, request: request, cancellationToken: cancellationToken);
        }

        public async Task<SuggestResponse<Address>> Geolocate(double lat, double lon, int radius_meters = 100,
            int count = 5, CancellationToken cancellationToken = default)
        {
            var request = new GeolocateRequest(lat, lon, radius_meters, count);
            return await Geolocate(request, cancellationToken);
        }

        public async Task<SuggestResponse<Address>> Geolocate(GeolocateRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Geolocate,
                entity: SuggestionsEntity.Address, request: request, cancellationToken: cancellationToken);
        }

        public async Task<IplocateResponse> Iplocate(string ip, string language = "ru",
            CancellationToken cancellationToken = default)
        {
            var parameters = new NameValueCollection
            {
                { "ip", ip },
                { "language", language }
            };
            return await ExecuteGet<IplocateResponse>(
                method: SuggestionsMethod.Iplocate,
                entity: SuggestionsEntity.Address,
                parameters: parameters,
                cancellationToken: cancellationToken);
        }

        #endregion

        #region Bank

        public async Task<SuggestResponse<Bank>> SuggestBank(string query, int count = 5, CancellationToken cancellationToken = default)
        {
            var request = new SuggestBankRequest(query, count);
            return await SuggestBank(request, cancellationToken);
        }

        public async Task<SuggestResponse<Bank>> SuggestBank(SuggestBankRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Bank>>(method: SuggestionsMethod.Suggest,
                entity: SuggestionsEntity.Bank, request: request, cancellationToken: cancellationToken);
        }

        public async Task<SuggestResponse<Bank>> FindBank(string query, CancellationToken cancellationToken = default)
        {
            var request = new FindBankRequest(query);
            return await FindBank(request, cancellationToken);
        }

        public async Task<SuggestResponse<Bank>> FindBank(FindBankRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Bank>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Bank,
                request: request, cancellationToken: cancellationToken);
        }

        #endregion

        #region Email

        public async Task<SuggestResponse<Email>> SuggestEmail(string query, int count = 5,
            CancellationToken cancellationToken = default)
        {
            var request = new SuggestRequest(query, count);
            return await SuggestEmail(request, cancellationToken);
        }

        public async Task<SuggestResponse<Email>> SuggestEmail(SuggestRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Email>>(method: SuggestionsMethod.Suggest,
                entity: SuggestionsEntity.Email, request: request, cancellationToken: cancellationToken);
        }

        #endregion

        #region Fias

        public async Task<SuggestResponse<FiasAddress>> SuggestFias(string query, int count = 5,
            CancellationToken cancellationToken = default)
        {
            var request = new SuggestAddressRequest(query, count);
            return await SuggestFias(request, cancellationToken);
        }

        public async Task<SuggestResponse<FiasAddress>> SuggestFias(SuggestAddressRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<FiasAddress>>(method: SuggestionsMethod.Suggest,
                entity: SuggestionsEntity.Fias, request: request, cancellationToken: cancellationToken);
        }

        public async Task<SuggestResponse<FiasAddress>> FindFias(string query, CancellationToken cancellationToken = default)
        {
            var request = new SuggestRequest(query);
            return await FindFias(request, cancellationToken);
        }

        public async Task<SuggestResponse<FiasAddress>> FindFias(SuggestRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<FiasAddress>>(method: SuggestionsMethod.Find,
                entity: SuggestionsEntity.Fias, request: request, cancellationToken: cancellationToken);
        }

        #endregion

        #region Name

        public async Task<SuggestResponse<Fullname>> SuggestName(string query, int count = 5,
            CancellationToken cancellationToken = default)
        {
            var request = new SuggestNameRequest(query, count);
            return await SuggestName(request, cancellationToken);
        }

        public async Task<SuggestResponse<Fullname>> SuggestName(SuggestNameRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Fullname>>(method: SuggestionsMethod.Suggest,
                entity: SuggestionsEntity.Name, request: request, cancellationToken: cancellationToken);
        }

        #endregion

        #region Party

        public async Task<SuggestResponse<Party>> SuggestParty(string query, int count = 5,
            CancellationToken cancellationToken = default)
        {
            var request = new SuggestPartyRequest(query, count);
            return await SuggestParty(request, cancellationToken);
        }

        public async Task<SuggestResponse<Party>> SuggestParty(SuggestPartyRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Party>>(method: SuggestionsMethod.Suggest,
                entity: SuggestionsEntity.Party, request: request, cancellationToken: cancellationToken);
        }

        public async Task<SuggestResponse<Party>> FindParty(string query, CancellationToken cancellationToken = default)
        {
            var request = new FindPartyRequest(query);
            return await FindParty(request, cancellationToken);
        }

        public async Task<SuggestResponse<Party>> FindParty(FindPartyRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Party>>(method: SuggestionsMethod.Find,
                entity: SuggestionsEntity.Party, request: request, cancellationToken: cancellationToken);
        }

        public async Task<SuggestResponse<Party>> FindAffiliated(string query,
            CancellationToken cancellationToken = default)
        {
            var request = new FindAffiliatedRequest(query);
            return await FindAffiliated(request, cancellationToken);
        }

        public async Task<SuggestResponse<Party>> FindAffiliated(FindAffiliatedRequest request,
            CancellationToken cancellationToken = default)
        {
            return await Execute<SuggestResponse<Party>>(method: SuggestionsMethod.FindAffiliated,
                entity: SuggestionsEntity.Party, request: request, cancellationToken: cancellationToken);
        }

        #endregion
    }
}
