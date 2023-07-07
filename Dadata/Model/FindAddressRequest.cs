namespace Dadata.Model
{
    public class FindAddressRequest : SuggestRequest
    {
        public AddressDivision division { get; set; }
        public string language { get; set; }

        public FindAddressRequest(string query, int count = 1) : base(query, count) { }
    }
}
