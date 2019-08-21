using System;
namespace Dadata.Model
{
    public class FindByIdPartyRequest : SuggestRequest
    {
        public string kpp { get; set; }
        public PartyType? type { get; set; }
        public FindByIdPartyRequest(string query, string kpp = null, int count = 1) : base(query: query, count) {
            this.kpp = kpp;
        }
    }
}
