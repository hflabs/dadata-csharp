using System;
using System.Collections.Specialized;
using Dadata.Model;

namespace Dadata
{
    public class ProfileClient : ClientBase
    {
        private const string BASE_URL = "https://dadata.ru/api/v2";

        public ProfileClient(string token, string secret, string baseUrl=BASE_URL) : base(token, secret, baseUrl) { }

        public DailyStatsResponse DailyStats()
        {
            return DailyStats(DateTime.Today);
        }

        public DailyStatsResponse DailyStats(DateTime date)
        {
            var parameters = new NameValueCollection(1);
            parameters.Add("date", date.ToString("yyyy-MM-dd"));
            return ExecuteGet<DailyStatsResponse>("stat", "daily", parameters);
        }
    }
}
