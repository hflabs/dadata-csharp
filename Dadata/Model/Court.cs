namespace Dadata.Model
{
    public class Court : IOutward
    {
        public string code { get; set; }
        public string name { get; set; }
        public string inn { get; set; }
        public string court_type { get; set; }
        public string court_type_name { get; set; }
        public string address { get; set; }
        public string legal_address { get; set; }
        public string website { get; set; }
    }
}
