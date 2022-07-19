namespace Dadata.Model
{
    public class SuggestAddressRequest : SuggestRequest
    {
        public Address[] locations { get; set; }
        public Address[] locations_boost { get; set; }
        public LocationGeo[] locations_geo { get; set; }
        public AddressBound from_bound { get; set; }
        public AddressBound to_bound { get; set; }
        public bool restrict_value { get; set; }
        public AddressDivision division { get; set; }
        public string language { get; set; }

        public SuggestAddressRequest(string query, int count = 5) : base(query, count) { }
    }

    public class AddressBound
    {
        public string value { get; set; }
        public AddressBound(string name)
        {
            this.value = name;
        }
    }

    public enum AddressDivision
    {
        ADMINISTRATIVE,
        MUNICIPAL
    }
}
