using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Dadata.Test
{
    public class SerializeTest
    {
        public SuggestClientAsync api { get; set; }

        public SerializeTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            api = new SuggestClientAsync(token);
        }

        [Fact]
        public async Task SerializeResponseTest()
        {
            var response = await api.FindParty("7707083893");
            var json = JsonConvert.SerializeObject(response.suggestions);
            Assert.Contains("\"value\":\"ПАО СБЕРБАНК\"", json);
            Assert.Contains("\"registration_date\":677376000000", json);
            Assert.Contains("\"liquidation_date\":null", json);
        }
    }
}
