using System;
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
        public void DailyStatsTest()
        {
            var response = api.DailyStats();
            Assert.Equal(DateTime.Today, response.date);
        }
    }
}
