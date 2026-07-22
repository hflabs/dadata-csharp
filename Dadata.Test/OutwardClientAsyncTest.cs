using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class OutwardClientAsyncTest
    {
        public OutwardClientAsync api { get; set; }

        public OutwardClientAsyncTest()
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
            var response = await api.Find<FnsUnit>("5252");
            var unit = response.suggestions[0].data;
            Assert.Equal("5252", unit.code);
            Assert.Contains("Нижегородской области", unit.name);
            Assert.Equal("40102810445370000059", unit.bank_correspondent_account);
        }

        [Fact]
        public async Task SuggestPostalUnit()
        {
            var response = await api.Suggest<PostalUnit>("дежнева 2а");
            var unit = response.suggestions[0].data;
            Assert.Equal("127081", unit.postal_code);
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
            var unit = response.suggestions[1].data;
            Assert.Equal("915354", unit.postal_code);
        }

        [Fact]
        public async Task FindDelivery()
        {
            var response = await api.Find<DeliveryCity>("3100400100000");
            var city = response.suggestions[0].data;
            Assert.Equal("7720193", city.boxberry_id);
            Assert.Equal("344", city.cdek_id);
            Assert.Equal("196006461", city.dpd_id);
        }

        [Fact]
        public async Task SuggestMetro()
        {
            var response = await api.Suggest<MetroStation>("александр");
            var station = response.suggestions[0].data;
            Assert.Equal("Александровский сад", station.name);
            Assert.Null(station.is_closed);
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
        public async Task FindOktmo()
        {
            var response = await api.Find<OktmoRecord>("54623425");
            var record = response.suggestions[0].data;
            Assert.Equal("54623425", record.oktmo);
        }

        [Fact]
        public async Task SuggestOkved2()
        {
            var response = await api.Suggest<OkvedRecord>("51.22.3");
            var record = response.suggestions[0].data;
            Assert.Equal("H51.22.3", record.idx);
        }

        [Fact]
        public async Task SuggestCountry()
        {
            var response = await api.Suggest<Country>("ru");
            var country = response.suggestions[0].data;
            Assert.Equal("Россия", country.name_short);
        }

        [Fact]
        public async Task SuggestCurrency()
        {
            var response = await api.Suggest<Currency>("rub");
            var currency = response.suggestions[0].data;
            Assert.Equal("Российский рубль", currency.name);
        }

        [Fact]
        public async Task SuggestFtsUnit()
        {
            var response = await api.Suggest<FtsUnit>("10002000");
            var unit = response.suggestions[0].data;
            Assert.Equal("ДОМОДЕДОВСКАЯ ТАМОЖНЯ", unit.name);
        }

        [Fact]
        public async Task SuggestMktu()
        {
            var response = await api.Suggest<MktuRecord>("валенки");
            var record = response.suggestions[0].data;
            Assert.Equal("250173", record.number);
            Assert.Equal("25", record.@class);
        }

        [Fact]
        public async Task SuggestOkpd2()
        {
            var response = await api.Suggest<OkpdRecord>("калоши");
            var record = response.suggestions[0].data;
            Assert.Equal("95.23.10.133", record.kod);
        }

        [Fact]
        public async Task SuggestRegionCourt()
        {
            var response = await api.Suggest<RegionCourt>("52MS0022");
            var court = response.suggestions[0].data;
            Assert.StartsWith("Судебный участок № 1", court.name);
        }

        [Fact]
        public async Task SuggestPartyBy()
        {
            var response = await api.Suggest<PartyBy>("онлайнер");
            var party = response.suggestions[0].data;
            Assert.Equal("190657494", party.unp);
            Assert.Equal(PartyByType.LEGAL, party.type);
            Assert.Equal(PartyByStatus.ACTIVE, party.status);
        }

        [Fact]
        public async Task SuggestPartyKz()
        {
            var response = await api.Suggest<PartyKz>("онлайн бухгалтерия");
            var party = response.suggestions[0].data;
            Assert.Equal("230940037236", party.bin);
            Assert.Equal(PartyKzType.LEGAL, party.type);
            Assert.Equal(PartyKzStatus.ACTIVE, party.status);
        }

        [Fact]
        public async Task FindCompanyBrand()
        {
            var baseUrl = "https://api.dadata.ru";
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var secret = Environment.GetEnvironmentVariable("DADATA_SECRET_KEY");
            api = new OutwardClientAsync(token, secret, baseUrl);

            var response = await api.Find<CompanyBrand>("560404424247");
            var brand = response.suggestions[0].data;
            Assert.Equal("roboshkola.com", brand.domain);
            Assert.Equal("РобоШкола", brand.name);
            Assert.Equal("560404424247", brand.inn);
            Assert.Equal("https://vk.com/roboshkola24", brand.vk_url);
            Assert.Contains("cdn.dadata.ru/logo/roboshkola.com", brand.logo_url);
        }
    }
}
