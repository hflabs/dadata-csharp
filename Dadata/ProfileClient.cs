namespace Dadata
{
    public class ProfileClient : ProfileClientSync
    {
        public ProfileClient(string token, string secret, string baseUrl = BASE_URL) : base(token, secret, baseUrl) { }
    }
}
