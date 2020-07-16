using System;
using Xunit;

namespace Dadata.Test
{
    public class IplocateClientTest
    {
        public SuggestClient api { get; set; }

        public IplocateClientTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new SuggestClient(token);
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
