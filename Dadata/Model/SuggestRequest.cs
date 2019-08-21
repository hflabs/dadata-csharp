using System;
namespace Dadata.Model
{
    public class SuggestRequest
    {
        public string query { get; set; }
        public int count { get; set; }

        public SuggestRequest(string query, int count=5)
        {
            this.query = query;
            this.count = count;
        }
    }
}
