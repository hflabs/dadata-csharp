using System;

namespace Dadata.Model
{
    public class SuggestBankRequest: SuggestRequest
    {
        public PartyStatus[] status { get; set; }
        public BankType[] type { get; set; }
        public SuggestBankRequest(string query, int count = 5) : base(query, count) { }
    }
}
