namespace Dadata.Model
{
    public class CompanyWithEmail
    {
        public Email email { get; set; }
        public Company company { get; set; }
    }

    public class Company
    {
        public string domain { get; set; }
        public string name { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public string okved { get; set; }
        public string okved_name { get; set; }
        public string employee_count { get; set; }
        public string income { get; set; }
        public string city { get; set; }
        public string timezone { get; set; }
    }
}
