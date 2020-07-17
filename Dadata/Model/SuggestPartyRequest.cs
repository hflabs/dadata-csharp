namespace Dadata.Model
{
    public class SuggestPartyRequest : SuggestRequest
    {
        public Address[] locations { get; set; }
        public Address[] locations_boost { get; set; }
        public PartyStatus[] status { get; set; }
        public PartyType? type { get; set; }
        public SuggestPartyRequest(string query, int count = 5) : base(query, count) { }
    }
}
