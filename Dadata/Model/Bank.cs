using System;
namespace Dadata.Model
{
    public class Bank
    {
        public string bic { get; set; }
        public string swift { get; set; }
        public string registration_number { get; set; }
        public string correspondent_account { get; set; }

        public BankName name { get; set; }
        public string payment_city { get; set; }
        public string okpo { get; set; }
        public BankOpf opf { get; set; }
        public string phone { get; set; }
        public string rkc { get; set; }
        public PartyState state { get; set; }

        public Suggestion<Address> address { get; set; }

    }

    public class BankName
    {
        public string payment { get; set; }
        public string full { get; set; }
        public string @short { get; set; }
    }

    public class BankOpf
    {
        public BankType type { get; set; }
        public string full { get; set; }
        public string @short { get; set; }
    }

    public enum BankType
    {
        BANK,
        NKO,
        BANK_BRANCH,
        NKO_BRANCH,
        RKC,
        OTHER
    }
}
