using System;
using System.Threading.Tasks;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class SuggestClientAsyncTest
    {
        public SuggestClientAsync api { get; set; }

        public SuggestClientAsyncTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            this.api = new SuggestClientAsync(token);
        }

        [Fact]
        public async Task SuggestAddressTest()
        {
            var query = "москва турчанинов 6с2";
            var response = await api.SuggestAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.Equal("119034", address_data.postal_code);
            Assert.Equal("7704", address_data.tax_office);
            Assert.Equal("Парк культуры", address_data.metro[0].name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestAddressLanguageTest()
        {
            var request = new SuggestAddressRequest("samara metallurgov") { language = "en" };
            var response = await api.SuggestAddress(request);
            Assert.Equal("Russia, gorod Samara, prospekt Metallurgov", response.suggestions[0].value);
        }

        [Fact]
        public async Task SuggestAddressLocationsKladrTest()
        {
            var request = new SuggestAddressRequest("ватутина")
            {
                locations = new[] {
                    new Address() { kladr_id = "65" },
                }
            };
            var response = await api.SuggestAddress(request);
            Assert.Equal("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestAddressLocationsMultipleLocationsTest()
        {
            var request = new SuggestAddressRequest("зеленоград")
            {
                locations = new[]
                {
                    new Address() { kladr_id = "50" },
                    new Address() { kladr_id = "77" }
                }
            };
            var response = await api.SuggestAddress(request);
            Assert.Equal("Зеленоград", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestAddressLocationsFiasCityTest()
        {
            var request = new SuggestAddressRequest("ватутина")
            {
                locations = new[]
                {
                    // Южно-Сахалинск
                    new Address() { city_fias_id = "44388ad0-06aa-49b0-bbf9-1704629d1d68" }
                }
            };
            var response = await api.SuggestAddress(request);
            Assert.Equal("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestAddressLocationsGeoTest()
        {
            var request = new SuggestAddressRequest("сухонская ")
            {
                locations_geo = new[]
                {
                    new LocationGeo() { lat=59.244634, lon=39.913355, radius_meters=200}
                }
            };
            var response = await api.SuggestAddress(request);
            Assert.Equal("г Вологда, ул Сухонская", response.suggestions[0].value);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestAddressBoundsTest()
        {
            var request = new SuggestAddressRequest("ново")
            {
                from_bound = new AddressBound("city"),
                to_bound = new AddressBound("city")
            };
            var response = await api.SuggestAddress(request);
            Assert.Equal("Новосибирск", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestAddressHistoryTest()
        {
            var query = "москва хабар";
            var response = await api.SuggestAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.Equal("ул Черненко", address_data.history_values[0]);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task FindAddressTest()
        {
            var response = await api.FindAddress("95dbf7fb-0dd4-4a04-8100-4f6c847564b5");
            var address = response.suggestions[0].data;
            Assert.Equal("Москва", address.city);
            Assert.Equal("Сухонская", address.street);
        }

        [Fact]
        public async Task FindAddressLanguageTest()
        {
            var request = new FindAddressRequest("94b67f2f-f0f2-4a56-983b-90f0cec1d789") { language = "en" };
            var response = await api.FindAddress(request);
            var address = response.suggestions[0].data;
            Assert.Equal("Samara", address.city);
            Assert.Equal("Metallurgov", address.street);
        }

        [Fact]
        public async Task SuggestBankTypeTest()
        {
            var request = new SuggestBankRequest("юмани")
            {
                type = new[] { BankType.NKO }
            };
            var response = await api.SuggestBank(request);
            Assert.Equal("044525444", response.suggestions[0].data.bic);
            Assert.Equal(new DateTime(2012, 08, 02), response.suggestions[0].data.state.registration_date);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestBankOpfTest()
        {
            var response = await api.SuggestBank("044525974");  // Тинькофф
            Assert.Equal(BankType.BANK, response.suggestions[0].data.opf.type);

            response = await api.SuggestBank("044525444");  // Юмани
            Assert.Equal(BankType.NKO, response.suggestions[0].data.opf.type);

            response = await api.SuggestBank("046577964");  // Альфа Екатеринбург
            Assert.Equal(BankType.BANK_BRANCH, response.suggestions[0].data.opf.type);

            response = await api.SuggestBank("044525000");  // ГУ Банка России по ЦФО
            Assert.Equal(BankType.CBR, response.suggestions[0].data.opf.type);

            response = await api.SuggestBank("041236100");  // УФК по Астраханской области
            Assert.Equal(BankType.TREASURY, response.suggestions[0].data.opf.type);

            response = await api.SuggestBank("044501002");  // Операционный департамент БР
            Assert.Equal(BankType.OTHER, response.suggestions[0].data.opf.type);
        }

        [Fact]
        public async Task FindBankTest()
        {
            var response = await api.FindBank("044525974");
            var bank = response.suggestions[0].data;
            Assert.Equal("TICSRUMMXXX", bank.swift);
            Assert.Equal("044525000", bank.cbr.bic);

            response = await api.FindBank("011203901");
            bank = response.suggestions[0].data;
            Assert.Equal("40102810445370000017", bank.treasury_accounts[0]);

        }

        [Fact]
        public async Task FindBankWithKppTest()
        {
            var request = new FindBankRequest(query: "7728168971", kpp: "667102002");
            var response = await api.FindBank(request);
            var bank = response.suggestions[0].data;
            Assert.Equal("046577964", bank.bic);
            Assert.Equal("7728168971", bank.inn);
            Assert.Equal("667102002", bank.kpp);
        }

        [Fact]
        public async Task SuggestEmailTest()
        {
            var query = "anton@m";
            var response = await api.SuggestEmail(query);
            Assert.Equal("anton@mail.ru", response.suggestions[0].value);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestFiasTest()
        {
            var query = "москва турчанинов 6с2";
            var response = await api.SuggestFias(query);
            var address = response.suggestions[0].data;
            Assert.Equal("Москва", address.region);
            Assert.Equal("Турчанинов", address.street);
            Assert.Equal("6", address.house);
            Assert.Equal("стр", address.building_type);
            Assert.Equal("2", address.building);
            Assert.Equal("8c925e61-9173-48b3-999e-dc85c86d89e7", address.fias_id);
        }

        [Fact]
        public async Task FindFiasTest()
        {
            var response = await api.FindFias("95dbf7fb-0dd4-4a04-8100-4f6c847564b5");
            var address = response.suggestions[0].data;
            Assert.Equal("Москва", address.region);
            Assert.Equal("Сухонская", address.street);
        }

        [Fact]
        public async Task SuggestNameTest()
        {
            var query = "викт";
            var response = await api.SuggestName(query);
            Assert.Equal("Виктор", response.suggestions[0].data.name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestNamePartsTest()
        {
            var request = new SuggestNameRequest("викт")
            {
                parts = new[] { FullnamePart.SURNAME }
            };
            var response = await api.SuggestName(request);
            Assert.Equal("Викторова", response.suggestions[0].data.surname);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestNameGenderTest()
        {
            var request = new SuggestNameRequest("виктор")
            {
                gender = Gender.FEMALE
            };
            var response = await api.SuggestName(request);
            Assert.Equal("Виктория", response.suggestions[0].data.name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestPartyTest()
        {
            var query = "7707083893";
            var response = await api.SuggestParty(query);
            var party = response.suggestions[0];
            var address = response.suggestions[0].data.address;
            Assert.Equal("7707083893", party.data.inn);
            Assert.Equal("г Москва, ул Вавилова, д 19", address.value);
            Assert.Contains("ГОРОД МОСКВА, УЛИЦА ВАВИЛОВА, 19", address.data.source);
            Assert.Equal("Вавилова", address.data.street);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestPartyByOkvedTest()
        {
            var query = new SuggestPartyRequest("авиа") {
                okved = new[] { "85.22" }
            };
            var response = await api.SuggestParty(query);
            var party = response.suggestions[0];
            Assert.Equal("7712038455", party.data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestPartyFioTest()
        {
            var query = "780251009376";
            var response = await api.SuggestParty(query);
            var party = response.suggestions[0];
            Assert.Equal("Кораева", party.data.fio.surname);
            Assert.Equal("Вероника", party.data.fio.name);
            Assert.Equal("Сергеевна", party.data.fio.patronymic);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestPartyStatCodesTest()
        {
            var query = "7707083893";
            var response = await api.SuggestParty(query);
            var party = response.suggestions[0];
            Assert.Equal("45293554000", party.data.okato);
            Assert.Equal("45397000000", party.data.oktmo);
            Assert.Equal("00032537", party.data.okpo);
            Assert.Equal("4100104", party.data.okogu);
            Assert.Equal("41", party.data.okfs);
            Assert.Equal("2014", party.data.okved_type);
            Assert.Equal("64.19", party.data.okved);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestPartyStatusTest()
        {
            var request = new SuggestPartyRequest("4713008497")
            {
                status = new[] { PartyStatus.LIQUIDATED }
            };
            var response = await api.SuggestParty(request);
            Assert.Equal("4713008497", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestPartyStatusBankruptTest()
        {
            var request = new SuggestPartyRequest("6102019481")
            {
                status = new[] { PartyStatus.BANKRUPT }
            };
            var response = await api.SuggestParty(request);
            Assert.Equal("6102019481", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestPartyStateCodeTest()
        {
            var request = new SuggestPartyRequest("6102019481");
            var response = await api.SuggestParty(request);
            Assert.Equal("117", response.suggestions[0].data.state.code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task SuggestPartyTypeTest()
        {
            var request = new SuggestPartyRequest("лаукаитис витас")
            {
                type = PartyType.INDIVIDUAL
            };
            var response = await api.SuggestParty(request);
            Assert.Equal("773165008890", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async Task FindPartyTest()
        {
            var response = await api.FindParty("7719402047");
            var party = response.suggestions[0].data;
            Assert.Equal("МОТОРИКА", party.name.@short);
            Assert.Equal(PartyFounderShareType.PERCENT, party.founders[0].share.type);
            Assert.True(party.founders[0].share.value > 50);
        }

        [Fact]
        public async Task FindPartyWithKppTest()
        {
            var request = new FindPartyRequest(query: "7728168971", kpp: "667102002");
            var response = await api.FindParty(request);
            var party = response.suggestions[0].data;
            Assert.Equal("ФИЛИАЛ \"ЕКАТЕРИНБУРГСКИЙ\" АО \"АЛЬФА-БАНК\"", party.name.short_with_opf);
        }

        [Fact]
        public async Task FindPartyPredecessorsTest()
        {
            var response = await api.FindParty("7728168971");
            var party = response.suggestions[0].data;
            var predecessor = party.predecessors[1];
            Assert.Equal("1027800011139", predecessor.ogrn);
            Assert.Equal("7834002576", predecessor.inn);
            Assert.Contains("БАЛТИЙСКИЙ БАНК", predecessor.name);
        }

        [Fact]
        public async Task FindPartyLicenseTest()
        {
            var response = await api.FindParty("7704865540");
            var party = response.suggestions[0].data;
            Assert.NotEmpty(party.licenses);
            Assert.Equal("Фармацевтическая деятельность", party.licenses[0].activities[0]);
        }

        [Fact]
        public async Task FindPartyContactsTest()
        {
            var response = await api.FindParty("7719402047");
            var party = response.suggestions[0].data;
            Assert.Single(party.phones);
            Assert.Equal("+7 911 241 0309", party.phones[0].data.source);
            Assert.Single(party.emails);
            Assert.Equal("info@motorica.org", party.emails[0].data.source);
        }

        [Fact]
        public async Task FindAffiliatedTest()
        {
            var response = await api.FindAffiliated("7736207543");
            Assert.Equal("ООО \"ДЗЕН.ПЛАТФОРМА\"", response.suggestions[0].value);
        }

        [Fact]
        public async Task FindAffiliatedScopeTest()
        {
            var request = new FindAffiliatedRequest("773006366201")
            {
                scope = new[] { FindAffiliatedScope.MANAGERS }
            };
            var response = await api.FindAffiliated(request);
            Assert.Equal("ООО \"ЯНДЕКС\"", response.suggestions[0].value);
        }
    }
}
