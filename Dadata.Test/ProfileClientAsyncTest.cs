using System;
using System.Threading.Tasks;
using Xunit;

namespace Dadata.Test
{
    public class ProfileClientAsyncTest
    {
        public ProfileClientAsync api { get; set; }
        public IProfileClientAsync api_i { get; set; }

        public ProfileClientAsyncTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var secret = Environment.GetEnvironmentVariable("DADATA_SECRET_KEY");
            api = new ProfileClientAsync(token, secret);
            api_i = api;
        }

        [Fact]
        public async Task GetBalanceTest()
        {
            var response = await api.GetBalance();
            Assert.True(response.balance >= 0);
        }

        [Fact]
        public async Task GetDailyStatsTest()
        {
            var response = await api.GetDailyStats();

            Assert.Equal(DateTime.Today, response.date);
        }

        [Fact]
        public async Task GetVersionsTest()
        {
            var response = await api.GetVersions();
            var r = await api_i.GetVersions();
            Assert.StartsWith("stable", response.dadata.version);
            Assert.True(response.suggestions.resources.ContainsKey("ЕГРЮЛ"));
            Assert.True(response.factor.resources.ContainsKey("ФИАС"));
        }
    }
}
