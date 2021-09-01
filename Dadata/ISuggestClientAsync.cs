using System.Threading;
using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public interface ISuggestClientAsync
    {
        Task<SuggestResponse<Address>> SuggestAddress(string query, int count = 5, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Address>> SuggestAddress(SuggestAddressRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Address>> FindAddress(string query, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Address>> FindAddress(FindAddressRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Address>> Geolocate(double lat, double lon, int radius_meters = 100, int count = 5, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Address>> Geolocate(GeolocateRequest request, CancellationToken cancellationToken = default);
        Task<IplocateResponse> Iplocate(string ip, string language = "ru", CancellationToken cancellationToken = default);
        Task<SuggestResponse<Bank>> SuggestBank(string query, int count = 5, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Bank>> SuggestBank(SuggestBankRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Bank>> FindBank(string query, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Bank>> FindBank(FindBankRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Email>> SuggestEmail(string query, int count = 5, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Email>> SuggestEmail(SuggestRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<FiasAddress>> SuggestFias(string query, int count = 5, CancellationToken cancellationToken = default);
        Task<SuggestResponse<FiasAddress>> SuggestFias(SuggestAddressRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<FiasAddress>> FindFias(string query, CancellationToken cancellationToken = default);
        Task<SuggestResponse<FiasAddress>> FindFias(SuggestRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Fullname>> SuggestName(string query, int count = 5, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Fullname>> SuggestName(SuggestNameRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Party>> SuggestParty(string query, int count = 5, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Party>> SuggestParty(SuggestPartyRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Party>> FindParty(string query, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Party>> FindParty(FindPartyRequest request, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Party>> FindAffiliated(string query, CancellationToken cancellationToken = default);
        Task<SuggestResponse<Party>> FindAffiliated(FindAffiliatedRequest request, CancellationToken cancellationToken = default);
    }
}
