using System;
using System.Collections.Generic;
using Xunit;

namespace Dadata.Test
{
    public class ProfileClientTest
    {
        public ProfileClient api { get; set; }

        public ProfileClientTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var secret = Environment.GetEnvironmentVariable("DADATA_SECRET_KEY");
            api = new ProfileClient(token, secret);
        }

        [Fact]
        public void GetBalanceTest()
        {
            var response = api.GetBalance();
            Assert.True(response.balance >= 0);
        }

        [Fact]
        public void GetDailyStatsTest()
        {
            var response = api.GetDailyStats();
            Assert.Equal(DateTime.Today, response.date);
        }

        [Fact]
        public void GetVersionsTest()
        {
            var response = api.GetVersions();
            Assert.StartsWith("stable", response.dadata.version);
            Assert.True(response.suggestions.resources.ContainsKey("ЕГРЮЛ"));
            Assert.True(response.factor.resources.ContainsKey("ФИАС"));
        }
    }
}
