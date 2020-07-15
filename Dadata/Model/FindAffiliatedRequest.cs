using System;
namespace Dadata.Model
{
    public class FindAffiliatedRequest : SuggestRequest
    {
        public FindAffiliatedScope[] scope { get; set; }
        public FindAffiliatedRequest(string query, int count = 10) : base(query, count) { }
    }

    public enum FindAffiliatedScope
    {
        MANAGERS,
        FOUNDERS,
    }
}
