using System;
using System.Collections.Specialized;
using Dadata.Model;

namespace Dadata
{
    public class ProfileClient : ClientBase
    {
        private const string BASE_URL = "https://dadata.ru/api/v2";

        public ProfileClient(string token, string secret, string baseUrl=BASE_URL) : base(token, secret, baseUrl) { }


        public GetBalanceResponse GetBalance()
        {
            return ExecuteGet<GetBalanceResponse>(method: "profile", entity: "balance");
        }

        public GetDailyStatsResponse GetDailyStats()
        {
            return GetDailyStats(DateTime.Today);
        }

        public GetDailyStatsResponse GetDailyStats(DateTime date)
        {
            var parameters = new NameValueCollection(1);
            parameters.Add("date", date.ToString("yyyy-MM-dd"));
            return ExecuteGet<GetDailyStatsResponse>(method: "stat", entity: "daily", parameters: parameters);
        }

        public GetVersionsResponse GetVersions()
        {
            return ExecuteGet<GetVersionsResponse>(method: "version", entity: null);
        }

    }
}
