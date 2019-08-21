using System;
using System.Collections.Generic;
using NUnit.Framework;
using Dadata.Model;

namespace Dadata.Test
{
    [TestFixture]
    public class SuggestClientTest
    {
        public SuggestClient api { get; set; }

        [SetUp]
        public void SetUp()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new SuggestClient(token);
        }

        [Test]
        public void SuggestAddressTest()
        {
            var query = "москва турчанинов 6с2";
            var response = api.SuggestAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.AreEqual("119034", address_data.postal_code);
            Assert.AreEqual("7704", address_data.tax_office);
            Assert.AreEqual("Парк культуры", address_data.metro[0].name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestAddressLocationsKladrTest()
        {
            var query = new SuggestAddressRequest("ватутина");
            query.locations = new[] {
                new Address() { kladr_id = "65" },
            };
            var response = api.SuggestAddress(query);
            Assert.AreEqual("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestAddressLocationsMultipleLocationsTest()
        {
            var query = new SuggestAddressRequest("зеленоград");
            query.locations = new[] {
                new Address() { kladr_id = "50" },
                new Address() { kladr_id = "77" }
            };
            var response = api.SuggestAddress(query);
            Assert.AreEqual("Зеленоград", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestAddressLocationsFiasCityTest()
        {
            var query = new SuggestAddressRequest("ватутина");
            query.locations = new[] {
                // Южно-Сахалинск
                new Address() { city_fias_id = "44388ad0-06aa-49b0-bbf9-1704629d1d68" }
            };
            var response = api.SuggestAddress(query);
            Assert.AreEqual("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestAddressBoundsTest()
        {
            var query = new SuggestAddressRequest("ново");
            query.from_bound = new AddressBound("city");
            query.to_bound = new AddressBound("city");
            var response = api.SuggestAddress(query);
            Assert.AreEqual("Новосибирск", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestAddressHistoryTest()
        {
            var query = "москва хабар";
            var response = api.SuggestAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.AreEqual("ул Черненко", address_data.history_values[0]);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void FindByIdAddressTest()
        {
            var response = api.FindByIdAddress("95dbf7fb-0dd4-4a04-8100-4f6c847564b5");
            var address = response.suggestions[0].data;
            Assert.AreEqual(address.city, "Москва");
            Assert.AreEqual(address.street, "Сухонская");
        }

        [Test]
        public void SuggestBankTypeTest()
        {
            var query = new SuggestBankRequest("я");
            query.type = new BankType[] { BankType.NKO };
            var response = api.SuggestBank(query);
            Assert.AreEqual("044525444", response.suggestions[0].data.bic);
            Assert.AreEqual(new DateTime(2012, 08, 02), response.suggestions[0].data.state.registration_date);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void FindByIdBankTest()
        {
            var response = api.FindByIdBank("044525974");
            var bank = response.suggestions[0].data;
            Assert.AreEqual(bank.swift, "TICSRUMMXXX");
        }

        [Test]
        public void SuggestEmailTest()
        {
            var query = "anton@m";
            var response = api.SuggestEmail(query);
            Assert.AreEqual("anton@mail.ru", response.suggestions[0].value);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestFioTest()
        {
            var query = "викт";
            var response = api.SuggestName(query);
            Assert.AreEqual("Виктор", response.suggestions[0].data.name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestFioPartsTest()
        {
            var query = new SuggestNameRequest("викт");
            query.parts = new FullnamePart[] { FullnamePart.SURNAME };
            var response = api.SuggestName(query);
            Assert.AreEqual("Викторова", response.suggestions[0].data.surname);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestPartyTest()
        {
            var query = "7707083893";
            var response = api.SuggestParty(query);
            var party = response.suggestions[0];
            var address = response.suggestions[0].data.address;
            Assert.AreEqual("7707083893", party.data.inn);
            Assert.AreEqual("г Москва, ул Вавилова, д 19", address.value);
            Assert.AreEqual("117997, ГОРОД МОСКВА, УЛИЦА ВАВИЛОВА, 19", address.data.source);
            Assert.AreEqual("117312", address.data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestPartyStatusTest()
        {
            var query = new SuggestPartyRequest("витас");
            query.status = new PartyStatus[] { PartyStatus.LIQUIDATED };
            var response = api.SuggestParty(query);
            Assert.AreEqual("4713008497", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestPartyTypeTest()
        {
            var query = new SuggestPartyRequest("лаукаитис витас");
            query.type = PartyType.INDIVIDUAL;
            var response = api.SuggestParty(query);
            Assert.AreEqual("773165008890", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void FindByIdPartyTest()
        {
            var response = api.FindByIdParty("7719402047");
            var party = response.suggestions[0].data;
            Assert.AreEqual(party.name.@short, "МОТОРИКА");
        }
    }
}
