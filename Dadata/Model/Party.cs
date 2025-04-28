﻿using System;
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
        public string kpp_largest { get; set; }
        public string ogrn { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? ogrn_date { get; set; }
        public string hid { get; set; }

        public PartyManagement management { get; set; }
        public PartyName name { get; set; }
        public Fullname fio { get; set; }

        public string okato { get; set; }
        public string oktmo { get; set; }
        public string okpo { get; set; }
        public string okogu { get; set; }
        public string okfs { get; set; }
        public string okved { get; set; }
        public List<PartyOkved> okveds { get; set; }
        public string okved_type { get; set; }

        public PartyOpf opf { get; set; }
        public PartyState state { get; set; }
        public PartyType type { get; set; }

        public int? employee_count { get; set; }
        public bool? invalid { get; set; }

        public PartyFinance finance { get; set; }
        public PartyCapital capital { get; set; }
        public PartyAuthorities authorities { get; set; }
        public PartyCitizenship citizenship { get; set; }
        public PartyDocuments documents { get; set; }
        public List<PartyLicense> licenses { get; set; }

        public List<PartyFounder> founders { get; set; }
        public List<PartyManager> managers { get; set; }

        public List<PartyReference> predecessors { get; set; }
        public List<PartyReference> successors { get; set; }

        public Suggestion<Address> address { get; set; }

        public List<Suggestion<Phone>> phones { get; set; }
        public List<Suggestion<Email>> emails { get; set; }
    }

    public class PartyReference
    {
        public string ogrn { get; set; }
        public string inn { get; set; }
        public string name { get; set; }
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
        public PartyCodeUnit code { get; set; }
        public PartyNameUnit name { get; set; }
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
        public PartyDocument fts_report { get; set; }
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
        public decimal? revenue { get; set; }
        public decimal? debt { get; set; }
        public decimal? penalty { get; set; }
        public int? year { get; set; }
    }

    public class PartyFounder
    {
        public string hid { get; set; }
        public PartyFounderType type { get; set; }
        public PartyFounderShare share { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? start_date { get; set; }
        public PartyInvalidity invalidity { get; set; }
        public string inn { get; set; }
        public Fullname fio { get; set; }
        public string ogrn { get; set; }
        public string name { get; set; }
    }

    public class PartyFounderShare
    {
        public PartyFounderShareType type { get; set; }
        public decimal value { get; set; }
        public long numerator { get; set; }
        public long denominator { get; set; }
    }

    public enum PartyFounderShareType
    {
        PERCENT,
        DECIMAL,
        FRACTION
    }

    public enum PartyFounderType
    {
        LEGAL,
        PHYSICAL
    }

    public class PartyInvalidity
    {
        public PartyInvalidityCode code { get; set; }
        public PartyCourtDecision decision { get; set; }
    }

    public enum PartyInvalidityCode
    {
        PARTY,
        FTS,
        COURT,
        OTHER
    }

    public class PartyCourtDecision
    {
        public string court_name { get; set; }
        public string number { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime date { get; set; }
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
        public List<string> activities { get; set; }
        public List<string> addresses { get; set; }
    }

    public class PartyManagement
    {
        public string name { get; set; }
        public string post { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? start_date { get; set; }
        public string disqualified { get; set; }
    }

    public class PartyManager
    {
        public string hid { get; set; }
        public PartyManagerType type { get; set; }
        [JsonConverter(typeof(DateMillisConverter))]
        public DateTime? start_date { get; set; }
        public PartyInvalidity invalidity { get; set; }
        public string inn { get; set; }
        public Fullname fio { get; set; }
        public string post { get; set; }
        public string ogrn { get; set; }
        public string name { get; set; }
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
        public string code { get; set; }
    }

    public enum PartyStatus
    {
        ACTIVE,
        LIQUIDATING,
        LIQUIDATED,
        REORGANIZING,
        BANKRUPT
    }

    public enum PartyTaxSystem
    {
        AUSN,
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
