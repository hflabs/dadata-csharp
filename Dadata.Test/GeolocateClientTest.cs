using System;
using Xunit;
using Dadata;

namespace Dadata.Test
{
    public class GeolocateClientTest
    {
        public GeolocateClient api { get; set; }

        public GeolocateClientTest() {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new GeolocateClient(token);
        }

        [Fact]
        public void GeolocateTest()
        {
            var response = api.Geolocate(lat: 55.7366021, lon: 37.597643);
            var address = response.suggestions[0].data;
            Assert.Equal("Москва", address.city);
            Assert.Equal("Турчанинов", address.street);
        }

        [Fact]
        public void NotFoundTest()
        {
            var response = api.Geolocate(lat: 1, lon: 1);
            Assert.Equal(0, response.suggestions.Count);
        }
    }
}

