using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public interface ISuggestClientAsync
    {
        Task<SuggestResponse<Address>> SuggestAddress(string query, int count = 5);
        Task<SuggestResponse<Address>> SuggestAddress(SuggestAddressRequest request);
        Task<SuggestResponse<Address>> FindAddress(string query);
        Task<SuggestResponse<Address>> FindAddress(FindAddressRequest request);
        Task<SuggestResponse<Address>> Geolocate(double lat, double lon, int radius_meters = 100, int count = 5);
        Task<SuggestResponse<Address>> Geolocate(GeolocateRequest request);
        Task<IplocateResponse> Iplocate(string ip, string language = "ru");
        Task<SuggestResponse<Bank>> SuggestBank(string query, int count = 5);
        Task<SuggestResponse<Bank>> SuggestBank(SuggestBankRequest request);
        Task<SuggestResponse<Bank>> FindBank(string query);
        Task<SuggestResponse<Bank>> FindBank(FindBankRequest request);
        Task<SuggestResponse<Email>> SuggestEmail(string query, int count = 5);
        Task<SuggestResponse<Email>> SuggestEmail(SuggestRequest request);
        Task<SuggestResponse<FiasAddress>> SuggestFias(string query, int count = 5);
        Task<SuggestResponse<FiasAddress>> SuggestFias(SuggestAddressRequest request);
        Task<SuggestResponse<FiasAddress>> FindFias(string query);
        Task<SuggestResponse<FiasAddress>> FindFias(SuggestRequest request);
        Task<SuggestResponse<Fullname>> SuggestName(string query, int count = 5);
        Task<SuggestResponse<Fullname>> SuggestName(SuggestNameRequest request);
        Task<SuggestResponse<Party>> SuggestParty(string query, int count = 5);
        Task<SuggestResponse<Party>> SuggestParty(SuggestPartyRequest request);
        Task<SuggestResponse<Party>> FindParty(string query);
        Task<SuggestResponse<Party>> FindParty(FindPartyRequest request);
        Task<SuggestResponse<Party>> FindAffiliated(string query);
        Task<SuggestResponse<Party>> FindAffiliated(FindAffiliatedRequest request);
    }
}
