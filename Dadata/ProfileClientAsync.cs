using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public class ProfileClientAsync : ClientBaseAsync, IProfileClientAsync
    {
        protected const string BASE_URL = "https://dadata.ru/api/v2";

        public ProfileClientAsync(string token, string secret, string baseUrl = BASE_URL, HttpClient client = null)
            : base(token, secret, baseUrl, client) { }


        public async Task<GetBalanceResponse> GetBalance()
        {
            return await ExecuteGet<GetBalanceResponse>(method: "profile", entity: "balance");
        }

        public async Task<GetDailyStatsResponse> GetDailyStats()
        {
            return await GetDailyStats(DateTime.Today);
        }

        public async Task<GetDailyStatsResponse> GetDailyStats(DateTime date)
        {
            var parameters = new NameValueCollection(1);
            parameters.Add("date", date.ToString("yyyy-MM-dd"));
            return await ExecuteGet<GetDailyStatsResponse>(method: "stat", entity: "daily", parameters: parameters);
        }

        public async Task<GetVersionsResponse> GetVersions()
        {
            return await ExecuteGet<GetVersionsResponse>(method: "version", entity: null);
        }

    }
}
