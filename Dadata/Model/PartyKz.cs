namespace Dadata.Model
{
    public class PartyKz : IOutward
    {
        public string bin { get; set; }
        public string registration_date { get; set; }
        public PartyKzStatus status { get; set; }
        public PartyKzType type { get; set; }
        public string name_ru { get; set; }
        public string name_kz { get; set; }
        public string fio { get; set; }
        public string kato { get; set; }
        public string address_ru { get; set; }
        public string address_kz { get; set; }
        public string address_settlement_ru { get; set; }
        public string address_settlement_kz { get; set; }
        public string address_local { get; set; }
        public string oked { get; set; }
        public string oked_name_ru { get; set; }
        public string oked_name_kz { get; set; }
        public string krp { get; set; }
        public string krp_name_ru { get; set; }
        public string krp_name_kz { get; set; }
        public string kse { get; set; }
        public string kse_name_ru { get; set; }
        public string kse_name_kz { get; set; }
        public string kfs { get; set; }
        public string kfs_name_ru { get; set; }
        public string kfs_name_kz { get; set; }
    }

    public enum PartyKzStatus
    {
        ACTIVE,
        INACTIVE,
        LIQUIDATING,
        LIQUIDATED,
        SUSPENDED
    }

    public enum PartyKzType
    {
        LEGAL,
        BRANCH,
        INDIVIDUAL,
        INDIVIDUAL_JOINT_VENTURE,
        FOREIGN_BRANCH
    }
}
