using System;
using System.Collections.Generic;

namespace Dadata.Model
{
    public class SuggestOutwardRequest : SuggestRequest
    {
        public IDictionary<string, string> filters;
        public SuggestOutwardRequest(string query, int count = 5) : base(query, count) { }
    }
}
