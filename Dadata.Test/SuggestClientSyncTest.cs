using System;
using System.Collections.Generic;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class SuggestClientSyncTest
    {
        public SuggestClientSync api { get; set; }

        public SuggestClientSyncTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new SuggestClientSync(token);
        }

        [Fact]
        public void SuggestAddressTest()
        {
            var query = "москва турчанинов 6с2";
            var response = api.SuggestAddress(query, count: 1);
            var address_data = response.suggestions[0].data;
            Assert.Equal("119034", address_data.postal_code);
            Assert.Equal("7704", address_data.tax_office);
            Assert.Equal("Парк культуры", address_data.metro[0].name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressLanguageTest()
        {
            var request = new SuggestAddressRequest("samara metallurgov") { language = "en" };
            var response = api.SuggestAddress(request);
            Assert.Equal("Russia, gorod Samara, prospekt Metallurgov", response.suggestions[0].value);
        }

        [Fact]
        public void SuggestAddressLocationsKladrTest()
        {
            var request = new SuggestAddressRequest("ватутина")
            {
                locations = new[] {
                    new Address() { kladr_id = "65" },
                }
            };
            var response = api.SuggestAddress(request);
            Assert.Equal("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressLocationsMultipleLocationsTest()
        {
            var request = new SuggestAddressRequest("зеленоград")
            {
                locations = new[]
                {
                    new Address() { kladr_id = "50" },
                    new Address() { kladr_id = "77" }
                }
            };
            var response = api.SuggestAddress(request);
            Assert.Equal("Зеленоград", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressLocationsFiasCityTest()
        {
            var request = new SuggestAddressRequest("ватутина")
            {
                locations = new[]
                {
                    // Южно-Сахалинск
                    new Address() { city_fias_id = "44388ad0-06aa-49b0-bbf9-1704629d1d68" }
                }
            };
            var response = api.SuggestAddress(request);
            Assert.Equal("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressLocationsGeoTest()
        {
            var request = new SuggestAddressRequest("сухонская ")
            {
                locations_geo = new[]
                {
                    new LocationGeo() { lat=59.244634, lon=39.913355, radius_meters=200}
                }
            };
            var response = api.SuggestAddress(request);
            Assert.Equal("г Вологда, ул Сухонская", response.suggestions[0].value);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressBoundsTest()
        {
            var request = new SuggestAddressRequest("ново")
            {
                from_bound = new AddressBound("city"),
                to_bound = new AddressBound("city")
            };
            var response = api.SuggestAddress(request);
            Assert.Equal("Новосибирск", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressHistoryTest()
        {
            var query = "москва хабар";
            var response = api.SuggestAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.Equal("ул Черненко", address_data.history_values[0]);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void FindAddressTest()
        {
            var response = api.FindAddress("95dbf7fb-0dd4-4a04-8100-4f6c847564b5");
            var address = response.suggestions[0].data;
            Assert.Equal("Москва", address.city);
            Assert.Equal("Сухонская", address.street);
        }

        [Fact]
        public void FindAddressLanguageTest()
        {
            var request = new FindAddressRequest("94b67f2f-f0f2-4a56-983b-90f0cec1d789") { language = "en" };
            var response = api.FindAddress(request);
            var address = response.suggestions[0].data;
            Assert.Equal("Samara", address.city);
            Assert.Equal("Metallurgov", address.street);
        }

        [Fact]
        public void SuggestBankTypeTest()
        {
            var request = new SuggestBankRequest("юмани")
            {
                type = new[] { BankType.NKO }
            };
            var response = api.SuggestBank(request);
            Assert.Equal("044525444", response.suggestions[0].data.bic);
            Assert.Equal(new DateTime(2012, 08, 02), response.suggestions[0].data.state.registration_date);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void FindBankTest()
        {
            var response = api.FindBank("044525974");
            var bank = response.suggestions[0].data;
            Assert.Equal("TICSRUMMXXX", bank.swift);
        }

        [Fact]
        public void FindBankWithKppTest()
        {
            var request = new FindBankRequest(query: "7728168971", kpp: "667102002");
            var response = api.FindBank(request);
            var bank = response.suggestions[0].data;
            Assert.Equal("046577964", bank.bic);
            Assert.Equal("7728168971", bank.inn);
            Assert.Equal("667102002", bank.kpp);
        }

        [Fact]
        public void SuggestEmailTest()
        {
            var query = "anton@m";
            var response = api.SuggestEmail(query);
            Assert.Equal("anton@mail.ru", response.suggestions[0].value);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestFiasTest()
        {
            var query = "москва турчанинов 6с2";
            var response = api.SuggestFias(query);
            var address = response.suggestions[0].data;
            Assert.Equal("Москва", address.region);
            Assert.Equal("Турчанинов", address.street);
            Assert.Equal("6", address.house);
            Assert.Equal("стр", address.building_type);
            Assert.Equal("2", address.building);
        }

        [Fact]
        public void FindFiasTest()
        {
            var response = api.FindFias("95dbf7fb-0dd4-4a04-8100-4f6c847564b5");
            var address = response.suggestions[0].data;
            Assert.Equal("Москва", address.region);
            Assert.Equal("Сухонская", address.street);
        }

        [Fact]
        public void SuggestNameTest()
        {
            var query = "викт";
            var response = api.SuggestName(query);
            Assert.Equal("Виктор", response.suggestions[0].data.name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestNamePartsTest()
        {
            var request = new SuggestNameRequest("викт")
            {
                parts = new FullnamePart[] { FullnamePart.SURNAME }
            };
            var response = api.SuggestName(request);
            Assert.Equal("Викторова", response.suggestions[0].data.surname);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestNameGenderTest()
        {
            var request = new SuggestNameRequest("виктор")
            {
                gender = Gender.FEMALE
            };
            var response = api.SuggestName(request);
            Assert.Equal("Виктория", response.suggestions[0].data.name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestPartyTest()
        {
            var query = "7707083893";
            var response = api.SuggestParty(query);
            var party = response.suggestions[0];
            var address = response.suggestions[0].data.address;
            Assert.Equal("7707083893", party.data.inn);
            Assert.Equal("г Москва, ул Вавилова, д 19", address.value);
            Assert.Contains("ВАВИЛОВА", address.data.source);
            Assert.Equal("Вавилова", address.data.street);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestPartyStatusTest()
        {
            var request = new SuggestPartyRequest("4713008497")
            {
                status = new[] { PartyStatus.LIQUIDATED }
            };
            var response = api.SuggestParty(request);
            Assert.Equal("4713008497", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestPartyTypeTest()
        {
            var request = new SuggestPartyRequest("лаукаитис витас")
            {
                type = PartyType.INDIVIDUAL
            };
            var response = api.SuggestParty(request);
            Assert.Equal("773165008890", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void FindPartyTest()
        {
            var response = api.FindParty("7719402047");
            var party = response.suggestions[0].data;
            Assert.Equal("МОТОРИКА", party.name.@short);
        }

        [Fact]
        public void FindPartyWithKppTest()
        {
            var request = new FindPartyRequest(query: "7728168971", kpp: "667143001");
            var response = api.FindParty(request);
            var party = response.suggestions[0].data;
            Assert.Equal("ФИЛИАЛ \"ЕКАТЕРИНБУРГСКИЙ\" АО \"АЛЬФА-БАНК\"", party.name.full_with_opf);
        }

        [Fact]
        public void FindAffiliatedTest()
        {
            var response = api.FindAffiliated("7736207543");
            Assert.Equal("ООО \"МАРКЕТ.ТРЕЙД\"", response.suggestions[0].value);
        }

        [Fact]
        public void FindAffiliatedScopeTest()
        {
            var request = new FindAffiliatedRequest("771687831332")
            {
                scope = new[] { FindAffiliatedScope.MANAGERS }
            };
            var response = api.FindAffiliated(request);
            Assert.Equal("ООО \"ЯНДЕКС\"", response.suggestions[0].value);
        }
    }
}
