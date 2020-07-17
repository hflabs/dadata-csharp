using System;
using Xunit;

namespace Dadata.Test
{
    public class IplocateClientSyncTest
    {
        public SuggestClientSync api { get; set; }

        public IplocateClientSyncTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new SuggestClientSync(token);
        }

        [Fact]
        public void IplocateTest()
        {
            var response = api.Iplocate("213.180.193.3");
            Assert.Equal("Москва", response.location.data.city);
        }

        [Fact]
        public void LanguageTest()
        {
            var response = api.Iplocate("213.180.193.3", language: "en");
            Assert.Equal("Moscow", response.location.data.city);
        }

        [Fact]
        public void NotFoundTest()
        {
            var response = api.Iplocate("192.168.0.1");
            Assert.Null(response.location);
        }
    }
}
