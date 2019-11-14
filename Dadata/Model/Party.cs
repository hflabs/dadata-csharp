using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dadata.Model
{
    public class Party
    {
        public string branch_count { get; set; }
        public PartyBranchType branch_type { get; set; }

        public string inn { get; set; }
        public string kpp { get; set; }
        public string ogrn { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? ogrn_date { get; set; }
        public string hid { get; set; }

        public PartyManagement management { get; set; }
        public PartyName name { get; set; }

        public string okpo { get; set; }
        public string okved { get; set; }
        public List<PartyOkved> okveds { get; set; }
        public string okved_type { get; set; }

        public PartyOpf opf { get; set; }
        public PartyState state { get; set; }
        public PartyType type { get; set; }

        public int? employee_count { get; set; }

        public PartyFinance finance { get; set; }
        public PartyCapital capital { get; set; }
        public PartyAuthorities authorities { get; set; }
        public PartyCitizenship citizenship { get; set; }

        public List<PartyFounder> founders { get; set; }
        public List<PartyManager> managers { get; set; }


        public Suggestion<Address> address { get; set; }
    }

    public class PartyAuthorities
    {
        public PartyAuthority fts_registration { get; set; }
        public PartyAuthority fts_report { get; set; }
        public PartyAuthority pf { get; set; }
        public PartyAuthority sif { get; set; }
    }

    public class PartyAuthority
    {
        public string type { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public enum PartyBranchType
    {
        MAIN,
        BRANCH
    }

    public class PartyCapital
    {
        public string type { get; set; }
        public decimal? value { get; set; }
    }

    public class PartyCitizenship
    {
        public PartyNameUnit code { get; set; }
        public PartyCodeUnit name { get; set; }
    }

    public class PartyCodeUnit
    {
        public string numeric { get; set; }
        public string alpha_3 { get; set; }
    }

    public class PartyDocument
    {
        public string type { get; set; }
        public string series { get; set; }
        public string number { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? issue_date { get; set; }
        public string issue_authority { get; set; }
    }

    public class PartyDocuments
    {
        public PartyDocument fts_registration { get; set; }
        public PartyDocument pf_registration { get; set; }
        public PartyDocument sif_registration { get; set; }
        public PartySmb smb { get; set; }
    }

    public class PartyNameUnit
    {
        public string full { get; set; }
        public string @short { get; set; }
    }

    public class PartyFinance
    {
        public PartyTaxSystem? tax_system { get; set; }
        public decimal? income { get; set; }
        public decimal? expense { get; set; }
        public decimal? debt { get; set; }
        public decimal? penalty { get; set; }
    }

    public class PartyFounder
    {
        public string ogrn { get; set; }
        public string inn { get; set; }
        public string name { get; set; }
        public Fullname fio { get; set; }
        public string hid { get; set; }
        public PartyFounderType type { get; set; }
    }

    public enum PartyFounderType
    {
        LEGAL,
        PHYSICAL
    }

    public class PartyLicense
    {
        public string series { get; set; }
        public string number { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? issue_date { get; set; }
        public string issue_authority { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? suspend_date { get; set; }
        public string suspend_authority { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? valid_from { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? valid_to { get; set; }
        public string[] activities { get; set; }
        public string[] addresses { get; set; }
    }

    public class PartyManagement
    {
        public string name { get; set; }
        public string post { get; set; }
        public string disqualified { get; set; }
    }

    public class PartyManager
    {
        public string ogrn { get; set; }
        public string inn { get; set; }
        public string name { get; set; }
        public Fullname fio { get; set; }
        public string post { get; set; }
        public string hid { get; set; }
        public string type { get; set; }
    }

    public enum PartyManagerType
    {
        EMPLOYEE,
        FOREIGNER,
        LEGAL
    }

    public class PartyName
    {
        public string full_with_opf { get; set; }
        public string short_with_opf { get; set; }
        public string latin { get; set; }
        public string full { get; set; }
        public string @short { get; set; }
    }

    public class PartyOkved
    {
        public bool main { get; set; }
        public string type { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class PartyOpf
    {
        public string code { get; set; }
        public string full { get; set; }
        public string @short { get; set; }
    }

    public class PartySmb
    {
        public string type { get; set; }
        public PartySmbCategory category { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? issue_date { get; set; }
    }

    public enum PartySmbCategory
    {
        MICRO,
        SMALL,
        MEDIUM
    }

    public class PartyState
    {
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? actuality_date { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? registration_date { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? liquidation_date { get; set; }
        public PartyStatus status { get; set; }
    }

    public enum PartyStatus
    {
        ACTIVE,
        LIQUIDATING,
        LIQUIDATED,
        REORGANIZING
    }

    public enum PartyTaxSystem
    {
        ENVD,
        ENVD_ESHN,
        ESHN,
        SRP,
        USN,
        USN_ENVD
    }

    public enum PartyType
    {
        LEGAL,
        INDIVIDUAL
    }
}
