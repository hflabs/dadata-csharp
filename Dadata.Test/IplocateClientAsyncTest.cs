using System;
using System.Threading.Tasks;
using Xunit;

namespace Dadata.Test
{
    public class IplocateClientAsyncTest
    {
        public SuggestClientAsync api { get; set; }

        public IplocateClientAsyncTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new SuggestClientAsync(token);
        }

        [Fact]
        public async Task IplocateTest()
        {
            var response = await api.Iplocate("213.180.193.3");
            Assert.Equal("Москва", response.location.data.city);
        }

        [Fact]
        public async Task LanguageTest()
        {
            var response = await api.Iplocate("213.180.193.3", language: "en");
            Assert.Equal("Moscow", response.location.data.city);
        }

        [Fact]
        public async Task NotFoundTest()
        {
            var response = await api.Iplocate("192.168.0.1");
            Assert.Null(response.location);
        }
    }
}
