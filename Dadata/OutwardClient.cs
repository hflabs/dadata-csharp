namespace Dadata
{
    public class OutwardClient : OutwardClientSync
    {
        public OutwardClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }
    }
}
