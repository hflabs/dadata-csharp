using Dadata.Model;

namespace Dadata
{
    public interface ISuggestClientSync
    {
        SuggestResponse<Address> SuggestAddress(string query, int count = 5);
        SuggestResponse<Address> SuggestAddress(SuggestAddressRequest request);
        SuggestResponse<Address> FindAddress(string query);
        SuggestResponse<Address> FindAddress(FindAddressRequest request);
        SuggestResponse<Address> Geolocate(double lat, double lon, int radius_meters = 100, int count = 5);
        SuggestResponse<Address> Geolocate(GeolocateRequest request);
        IplocateResponse Iplocate(string ip, string language = "ru");
        SuggestResponse<Bank> SuggestBank(string query, int count = 5);
        SuggestResponse<Bank> SuggestBank(SuggestBankRequest request);
        SuggestResponse<Bank> FindBank(string query);
        SuggestResponse<Bank> FindBank(FindBankRequest request);
        SuggestResponse<Email> SuggestEmail(string query, int count = 5);
        SuggestResponse<Email> SuggestEmail(SuggestRequest request);
        SuggestResponse<FiasAddress> SuggestFias(string query, int count = 5);
        SuggestResponse<FiasAddress> SuggestFias(SuggestAddressRequest request);
        SuggestResponse<FiasAddress> FindFias(string query);
        SuggestResponse<FiasAddress> FindFias(SuggestRequest request);
        SuggestResponse<Fullname> SuggestName(string query, int count = 5);
        SuggestResponse<Fullname> SuggestName(SuggestNameRequest request);
        SuggestResponse<Party> SuggestParty(string query, int count = 5);
        SuggestResponse<Party> SuggestParty(SuggestPartyRequest request);
        SuggestResponse<Party> FindParty(string query);
        SuggestResponse<Party> FindParty(FindPartyRequest request);
        SuggestResponse<Party> FindAffiliated(string query);
        SuggestResponse<Party> FindAffiliated(FindAffiliatedRequest request);
    }
}
