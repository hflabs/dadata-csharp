using System;
using System.Threading.Tasks;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class GeolocateClientAsyncTest
    {
        public SuggestClientAsync api { get; set; }

        public GeolocateClientAsyncTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new SuggestClientAsync(token);
        }

        [Fact]
        public async Task GeolocateTest()
        {
            var response = await api.Geolocate(lat: 55.7366021, lon: 37.597643);
            var address = response.suggestions[0].data;
            Assert.Equal("Москва", address.city);
            Assert.Equal("Турчанинов", address.street);
        }

        [Fact]
        public async Task LanguageTest()
        {
            var request = new GeolocateRequest(lat: 55.7366021, lon: 37.597643) { language = "en" };
            var response = await api.Geolocate(request);
            var address = response.suggestions[0].data;
            Assert.Equal("Moscow", address.city);
            Assert.Equal("Turchaninov", address.street);
        }

        [Fact]
        public async Task NotFoundTest()
        {
            var response = await api.Geolocate(lat: 1, lon: 1);
            Assert.Equal(0, response.suggestions.Count);
        }
    }
}

