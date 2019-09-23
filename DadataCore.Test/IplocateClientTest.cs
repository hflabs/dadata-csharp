using System;
using NUnit.Framework;

namespace DadataCore.Test
{
    [TestFixture]
    public class IplocateClientTest
    {
        public IplocateClient api { get; set; }

        [SetUp]
        public void SetUp()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new IplocateClient(token);
        }

        [Test]
        public void IplocateTest()
        {
            var response = api.Iplocate("213.180.193.3");
            Assert.AreEqual(response.location.data.city, "Москва");
        }

        [Test]
        public void NotFoundTest()
        {
            var response = api.Iplocate("192.168.0.1");
            Assert.AreEqual(response.location, null);
        }
    }
}
