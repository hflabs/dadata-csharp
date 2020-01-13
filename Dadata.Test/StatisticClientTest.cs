using System;
using NUnit.Framework;

namespace Dadata.Test
{
    [TestFixture]
    public class StatisticClientTest {
        private StatisticClient _api;
        
        [SetUp]
        public void SetUp()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var secret = Environment.GetEnvironmentVariable("DADATA_SECRET_KEY");
            _api = new StatisticClient(token, secret);
        }

        [Test]
        public void SuggestAddressTest()
        {
            var response = _api.Daily(DateTime.Today);
            Assert.AreEqual(DateTime.Today, response.date);
        }
    }
}
