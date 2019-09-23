using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DadataCore.Test {
	[TestFixture]
	public class IplocateClientTest {
		public IplocateClient api { get; set; }

		[SetUp]
		public void SetUp() {
			var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
			this.api = new IplocateClient(token);
		}

		[Test]
		public async Task IplocateTest() {
			var response = await api.Iplocate("213.180.193.3");
			Assert.AreEqual(response.location.data.city, "Москва");
		}

		[Test]
		public async Task NotFoundTest() {
			var response = await api.Iplocate("192.168.0.1");
			Assert.AreEqual(response.location, null);
		}
	}
}
