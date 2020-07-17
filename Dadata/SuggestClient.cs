namespace Dadata
{
    public class SuggestClient : SuggestClientSync
    {
        public SuggestClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }
    }
}
