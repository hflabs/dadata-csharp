namespace Dadata
{
    public class CleanClient : CleanClientSync
    {
        public CleanClient(string token, string secret, string baseUrl = BASE_URL) : base(token, secret, baseUrl) { }
    }
}
