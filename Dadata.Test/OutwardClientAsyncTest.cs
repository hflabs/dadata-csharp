using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class OutwardClientTest
    {
        public OutwardClientAsync api { get; set; }

        public OutwardClientTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            api = new OutwardClientAsync(token);
        }

        [Fact]
        public async Task SuggestFmsUnit()
        {
            var response = await api.Suggest<FmsUnit>("772-053");
            var unit = response.suggestions[0].data;
            Assert.Equal("772-053", unit.code);
            Assert.Equal("ОВД ЗЮЗИНО Г. МОСКВЫ", unit.name);
        }

        [Fact]
        public async Task FilterFmsUnit()
        {
            var request = new SuggestOutwardRequest("иванов")
            {
                filters = new Dictionary<string, string>() { { "region_code", "37" } }
            };
            var response = await api.Suggest<FmsUnit>(request);
            var unit = response.suggestions[0].data;
            Assert.Equal("370-000", unit.code);
            Assert.Equal("ОВД ЛЕНИНСКОГО РАЙОНА Г. ИВАНОВО", unit.name);
        }

        [Fact]
        public async Task FindFnsUnit()
        {
            var response = await api.Find<FnsUnit>("5257");
            var unit = response.suggestions[0].data;
            Assert.Equal("5257", unit.code);
            Assert.StartsWith("Инспекция ФНС России", unit.name);
        }

        [Fact]
        public async Task SuggestPostalUnit()
        {
            var response = await api.Suggest<PostalUnit>("дежнева 2а");
            var unit = response.suggestions[0].data;
            Assert.Equal("127642", unit.postal_code);
        }

        [Fact]
        public async Task FindPostalUnit()
        {
            var response = await api.Find<PostalUnit>("127642");
            var unit = response.suggestions[0].data;
            Assert.Equal("127642", unit.postal_code);
        }

        [Fact]
        public async Task GeolocatePostalUnit()
        {
            var response = await api.Geolocate<PostalUnit>(lat: 55.878, lon: 37.653, radius_meters: 1000);
            var unit = response.suggestions[0].data;
            Assert.Equal("127642", unit.postal_code);
        }

        [Fact]
        public async Task FindDelivery()
        {
            var response = await api.Find<DeliveryCity>("3100400100000");
            var city = response.suggestions[0].data;
            Assert.Equal("01929", city.boxberry_id);
            Assert.Equal("344", city.cdek_id);
            Assert.Equal("196006461", city.dpd_id);
        }

        [Fact]
        public async Task SuggestMetro()
        {
            var response = await api.Suggest<MetroStation>("александр");
            var station = response.suggestions[0].data;
            Assert.Equal("Александровский сад", station.name);
            Assert.False(station.is_closed);
        }

        [Fact]
        public async Task FilterMetro()
        {
            var request = new SuggestOutwardRequest("александр")
            {
                filters = new Dictionary<string, string>() { { "city", "Санкт-Петербург" } }
            };
            var response = await api.Suggest<MetroStation>(request);
            var station = response.suggestions[0].data;
            Assert.Equal("Площадь Александра Невского 1", station.name);
        }

        [Fact]
        public async Task SuggestCarBrand()
        {
            var response = await api.Suggest<CarBrand>("FORD");
            var brand = response.suggestions[0].data;
            Assert.Equal("Форд", brand.name_ru);
        }

        [Fact]
        public async Task SuggestOkved2()
        {
            var response = await api.Suggest<OkvedRecord>("51.22.3");
            var record = response.suggestions[0].data;
            Assert.Equal("H.51.22.3", record.idx);
        }

        [Fact]
        public async Task SuggestCountry()
        {
            var response = await api.Suggest<Country>("ru");
            var country = response.suggestions[0].data;
            Assert.Equal("Россия", country.name_short);
        }
    }
}
