namespace Dadata.Model
{
    public class PartyBy : IOutward
    {
        public string unp { get; set; }
        public string registration_date { get; set; }
        public string removal_date { get; set; }
        public string actuality_date { get; set; }
        public PartyByStatus status { get; set; }
        public PartyByType type { get; set; }
        public string full_name_ru { get; set; }
        public string full_name_by { get; set; }
        public string short_name_ru { get; set; }
        public string short_name_by { get; set; }
        public string trade_name_ru { get; set; }
        public string trade_name_by { get; set; }
        public string fio_ru { get; set; }
        public string fio_by { get; set; }
        public string address { get; set; }
        public string oked { get; set; }
        public string oked_name { get; set; }
    }

    public enum PartyByStatus
    {
        ACTIVE,
        LIQUIDATING,
        LIQUIDATED,
        BANKRUPT,
        SUSPENDED,
        REORGANIZING
    }

    public enum PartyByType
    {
        INDIVIDUAL,
        LEGAL
    }
}
