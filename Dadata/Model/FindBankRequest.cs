namespace Dadata.Model
{
    public class FindBankRequest : SuggestRequest
    {
        public string kpp { get; set; }
        public FindBankRequest(string query, string kpp = null, int count = 1) : base(query, count)
        {
            this.kpp = kpp;
        }
    }
}
