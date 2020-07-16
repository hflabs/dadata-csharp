using System;

namespace Dadata.Model
{
    public class BalanceResponse : IDadataResponse
    {
        public Decimal balance { get; set; }
    }
}
