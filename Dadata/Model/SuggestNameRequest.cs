using System;
namespace Dadata.Model
{
    public class SuggestNameRequest: SuggestRequest
    {
        public FullnamePart[] parts { get; set; }
        public SuggestNameRequest(string query, int count = 5) : base(query, count) { }
    }

    public enum FullnamePart
    {
        SURNAME,
        NAME,
        PATRONYMIC
    }
}
