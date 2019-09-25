using System;
namespace Dadata.Model
{
    public class FindPartyRequest : SuggestRequest
    {
        public string kpp { get; set; }
        public PartyType? type { get; set; }
        public FindPartyRequest(string query, string kpp = null, int count = 1) : base(query, count) {
            this.kpp = kpp;
        }
    }
}
