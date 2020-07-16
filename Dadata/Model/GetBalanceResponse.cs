using System;

namespace Dadata.Model
{
    public class GetBalanceResponse : IDadataResponse
    {
        public Decimal balance { get; set; }
    }
}
