using System;
using Xunit;

namespace Dadata.Test
{
    public class IplocateClientTest
    {
        public IplocateClient api { get; set; }

        public IplocateClientTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new IplocateClient(token);
        }

        [Fact]
        public void IplocateTest()
        {
            var response = api.Iplocate("213.180.193.3");
            Assert.Equal("Москва", response.location.data.city);
        }

        [Fact]
        public void NotFoundTest()
        {
            var response = api.Iplocate("192.168.0.1");
            Assert.Null(response.location);
        }
    }
}
