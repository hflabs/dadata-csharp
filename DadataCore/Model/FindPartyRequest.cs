using System;
namespace DadataCore.Model
{
    public class FindPartyRequest : SuggestRequest
    {
        public string kpp { get; set; }
        public PartyType? type { get; set; }
        public FindPartyRequest(string query, string kpp = null, int count = 1) : base(query: query, count) {
            this.kpp = kpp;
        }
    }
}
