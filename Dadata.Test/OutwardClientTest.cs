using System;
using System.Collections.Generic;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class OutwardClientTest
    {
        public OutwardClient api { get; set; }

        public OutwardClientTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            api = new OutwardClient(token);
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
    }
}
