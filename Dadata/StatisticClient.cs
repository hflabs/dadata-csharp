using System;
using System.Collections.Specialized;
using Dadata.Model;

namespace Dadata {

    /// <summary>
    /// DaData Statistic API (https://dadata.ru/api/stat)
    /// </summary>
    public class StatisticClient : ClientBase
    {
        private const string BASE_URL= "https://dadata.ru/api/v2";

        public StatisticClient(string token, string secretKey, string baseUrl = BASE_URL) : base(token, secretKey, baseUrl) {
        }

        public StatisticResponse Daily(DateTime date) {
            var parameters = new NameValueCollection(1);
            parameters.Add("date", date.ToString("yyyy-MM-dd"));
            return ExecuteGet<StatisticResponse>("stat", "daily", parameters);
        }

    }
}
