using System;
using System.Collections.Generic;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class SuggestClientTest
    {
        public SuggestClient api { get; set; }

        public SuggestClientTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new SuggestClient(token);
        }

        [Fact]
        public void SuggestAddressTest()
        {
            var query = "москва турчанинов 6с2";
            var response = api.SuggestAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.Equal("119034", address_data.postal_code);
            Assert.Equal("7704", address_data.tax_office);
            Assert.Equal("Парк культуры", address_data.metro[0].name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressLocationsKladrTest()
        {
            var query = new SuggestAddressRequest("ватутина");
            query.locations = new[] {
                new Address() { kladr_id = "65" },
            };
            var response = api.SuggestAddress(query);
            Assert.Equal("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressLocationsMultipleLocationsTest()
        {
            var query = new SuggestAddressRequest("зеленоград");
            query.locations = new[] {
                new Address() { kladr_id = "50" },
                new Address() { kladr_id = "77" }
            };
            var response = api.SuggestAddress(query);
            Assert.Equal("Зеленоград", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressLocationsFiasCityTest()
        {
            var query = new SuggestAddressRequest("ватутина");
            query.locations = new[] {
                // Южно-Сахалинск
                new Address() { city_fias_id = "44388ad0-06aa-49b0-bbf9-1704629d1d68" }
            };
            var response = api.SuggestAddress(query);
            Assert.Equal("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestAddressBoundsTest()
        {
            var query = new SuggestAddressRequest("ново");
            query.from_bound = new AddressBound("city");
            query.to_bound = new AddressBound("city");
            var response = api.SuggestAddress(query);
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
        public void SuggestBankTypeTest()
        {
            var query = new SuggestBankRequest("я");
            query.type = new BankType[] { BankType.NKO };
            var response = api.SuggestBank(query);
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
        public void SuggestEmailTest()
        {
            var query = "anton@m";
            var response = api.SuggestEmail(query);
            Assert.Equal("anton@mail.ru", response.suggestions[0].value);
            Console.WriteLine(string.Join("\n", response.suggestions));
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
            Assert.Equal("117997, ГОРОД МОСКВА, УЛИЦА ВАВИЛОВА, 19", address.data.source);
            Assert.Equal("117312", address.data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestPartyStatusTest()
        {
            var query = new SuggestPartyRequest("витас");
            query.status = new PartyStatus[] { PartyStatus.LIQUIDATED };
            var response = api.SuggestParty(query);
            Assert.Equal("4713008497", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void SuggestPartyTypeTest()
        {
            var query = new SuggestPartyRequest("лаукаитис витас");
            query.type = PartyType.INDIVIDUAL;
            var response = api.SuggestParty(query);
            Assert.Equal("773165008890", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public void FindPartyTest()
        {
            var response = api.FindParty("7719402047");
            var party = response.suggestions[0].data;
            Assert.Equal("МОТОРИКА", party.name.@short);
            Assert.Equal(PartyFounderShareType.PERCENT, party.founders[0].share.type);
            Assert.Equal(100, party.founders[0].share.value);
        }

        [Fact]
        public void FindPartyWithKppTest()
        {
            var request = new FindPartyRequest(query: "7728168971", kpp: "667102002");
            var response = api.FindParty(request);
            var party = response.suggestions[0].data;
            Assert.Equal("ФИЛИАЛ \"ЕКАТЕРИНБУРГСКИЙ\" АО \"АЛЬФА-БАНК\"", party.name.short_with_opf);
        }

        [Fact]
        public void FindAffiliatedTest()
        {
            var response = api.FindAffiliated("7736207543");
            Assert.Equal("ООО \"ДЗЕН.ПЛАТФОРМА\"", response.suggestions[0].value);
        }

        [Fact]
        public void FindAffilliatedScopeTest()
        {
            var request = new FindAffiliatedRequest("773006366201");
            request.scope = new FindAffiliatedScope[] { FindAffiliatedScope.MANAGERS };
            var response = api.FindAffiliated(request);
            Assert.Equal("ООО \"ЯНДЕКС\"", response.suggestions[0].value);
        }
    }
}
