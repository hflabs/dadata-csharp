using System;
using System.Collections.Generic;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class OutwardClientSyncTest
    {
        public OutwardClientSync api { get; set; }

        public OutwardClientSyncTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            api = new OutwardClientSync(token);
        }

        [Fact]
        public void SuggestFmsUnit()
        {
            var response = api.Suggest<FmsUnit>("772-053");
            var unit = response.suggestions[0].data;
            Assert.Equal("772-053", unit.code);
            Assert.Equal("ОВД ЗЮЗИНО Г. МОСКВЫ", unit.name);
        }

        [Fact]
        public void FilterFmsUnit()
        {
            var request = new SuggestOutwardRequest("иванов")
            {
                filters = new Dictionary<string, string>() { { "region_code", "37" } }
            };
            var response = api.Suggest<FmsUnit>(request);
            var unit = response.suggestions[0].data;
            Assert.Equal("370-000", unit.code);
            Assert.Equal("ОВД ЛЕНИНСКОГО РАЙОНА Г. ИВАНОВО", unit.name);
        }

        [Fact]
        public void FindFnsUnit()
        {
            var response = api.Find<FnsUnit>("5257");
            var unit = response.suggestions[0].data;
            Assert.Equal("5257", unit.code);
            Assert.Contains("Нижегородской области", unit.name);
            Assert.Equal("40102810745370000024", unit.bank_correspondent_account);
        }

        [Fact]
        public void SuggestPostalUnit()
        {
            var response = api.Suggest<PostalUnit>("дежнева 2а");
            var unit = response.suggestions[0].data;
            Assert.Equal("127642", unit.postal_code);
        }

        [Fact]
        public void FindPostalUnit()
        {
            var response = api.Find<PostalUnit>("127642");
            var unit = response.suggestions[0].data;
            Assert.Equal("127642", unit.postal_code);
        }

        [Fact]
        public void GeolocatePostalUnit()
        {
            var response = api.Geolocate<PostalUnit>(lat: 55.878, lon: 37.653, radius_meters: 1000);
            var unit = response.suggestions[0].data;
            Assert.Equal("127642", unit.postal_code);
        }

        [Fact]
        public void FindDelivery()
        {
            var response = api.Find<DeliveryCity>("3100400100000");
            var city = response.suggestions[0].data;
            Assert.Equal("01929", city.boxberry_id);
            Assert.Equal("344", city.cdek_id);
            Assert.Equal("196006461", city.dpd_id);
        }

        [Fact]
        public void SuggestMetro()
        {
            var response = api.Suggest<MetroStation>("александр");
            var station = response.suggestions[0].data;
            Assert.Equal("Александровский сад", station.name);
            Assert.False(station.is_closed);
        }

        [Fact]
        public void FilterMetro()
        {
            var request = new SuggestOutwardRequest("александр")
            {
                filters = new Dictionary<string, string>() { { "city", "Санкт-Петербург" } }
            };
            var response = api.Suggest<MetroStation>(request);
            var station = response.suggestions[0].data;
            Assert.Equal("Площадь Александра Невского 1", station.name);
        }

        [Fact]
        public void SuggestCarBrand()
        {
            var response = api.Suggest<CarBrand>("FORD");
            var brand = response.suggestions[0].data;
            Assert.Equal("Форд", brand.name_ru);
        }

        [Fact]
        public void SuggestOkved2()
        {
            var response = api.Suggest<OkvedRecord>("51.22.3");
            var record = response.suggestions[0].data;
            Assert.Equal("H.51.22.3", record.idx);
        }

        [Fact]
        public void SuggestCountry()
        {
            var response = api.Suggest<Country>("ru");
            var country = response.suggestions[0].data;
            Assert.Equal("Россия", country.name_short);
        }
    }
}
