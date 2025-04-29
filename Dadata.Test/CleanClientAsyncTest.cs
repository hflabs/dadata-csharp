using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class CleanClientAsyncTest
    {
        public CleanClientAsync api { get; set; }

        public CleanClientAsyncTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var secret = Environment.GetEnvironmentVariable("DADATA_SECRET_KEY");
            api = new CleanClientAsync(token, secret);
        }

        [Fact]
        public async Task CleanAsIsTest()
        {
            var cleaned = await api.Clean<AsIs>("Омномном");
            Assert.Equal("Омномном", cleaned.source);
        }

        [Fact]
        public async Task CleanAddressTest()
        {
            var cleaned = await api.Clean<Address>("москва курьяновская наб 17");
            Assert.Equal("Москва", cleaned.region);
            Assert.Null(cleaned.city);
            Assert.Equal("Печатники", cleaned.city_district);
            Assert.Equal("Курьяновская", cleaned.settlement);
            Assert.Null(cleaned.street);
            Assert.Equal("17", cleaned.house);
            Assert.Equal("65", cleaned.fias_level);
            Assert.Equal("0", cleaned.qc);
            Assert.Equal(3, cleaned.metro.Count);
            Assert.Null(cleaned.divisions.administrative.area);
            Assert.Null(cleaned.divisions.administrative.city);
            Assert.Equal("Печатники", cleaned.divisions.administrative.city_district.name);
            Assert.Null(cleaned.divisions.administrative.settlement);
            Assert.Equal("Курьяновская", cleaned.divisions.administrative.planning_structure.name);
            Assert.Null(cleaned.divisions.municipal);
        }

        [Fact]
        public async Task CleanAddressRoomTest()
        {
            var cleaned = await api.Clean<Address>("г Москва, Куркинское шоссе, д 17, кв 173, ком 2");
            Assert.Equal("17", cleaned.house);
            Assert.Equal("253", cleaned.house_flat_count);
            Assert.Equal("173", cleaned.flat);
            Assert.Equal("2", cleaned.room);
        }

        [Fact]
        public async Task CleanBirthdateTest()
        {
            var cleaned = await api.Clean<Birthdate>("12.03.1990");
            Assert.Equal(new DateTime(1990, 3, 12), cleaned.birthdate);
            Assert.Equal("0", cleaned.qc);
        }

        [Fact]
        public async Task CleanEmailTest()
        {
            var cleaned = await api.Clean<Email>("anderson@matrix.ru");
            Assert.Equal("anderson@matrix.ru", cleaned.email);
            Assert.Equal("CORPORATE", cleaned.type);
            Assert.Equal("0", cleaned.qc);

        }

        [Fact]
        public async Task CleanNameTest()
        {
            var cleaned = await api.Clean<Fullname>("Ольга Викторовна Раздербань");
            Assert.Equal("Ольга", cleaned.name);
            Assert.Equal("0", cleaned.qc);
        }

        [Fact]
        public async Task CleanPhoneTest()
        {
            var cleaned = await api.Clean<Phone>("89168459285");
            Assert.Equal("8459285", cleaned.number);
            Assert.Equal("Россия", cleaned.country);
            Assert.Equal("0", cleaned.qc);
        }

        [Fact]
        public async Task CleanPassportTest()
        {
            var cleaned = await api.Clean<Passport>("4506 629672");
            Assert.Equal("45 06", cleaned.series);
            Assert.Equal("10", cleaned.qc);
        }

        [Fact]
        public async Task CleanTest()
        {
            var structure = new[] { StructureType.NAME, StructureType.ADDRESS };
            var data = new[] { "Кузнецов Петр Алексеич", "Москва Милютинский 13" };

            var cleaned = await api.Clean(structure, data);
            Assert.Equal(2, cleaned.Count);

            Assert.IsType<Fullname>(cleaned[0]);
            var firstName = (Fullname)cleaned[0];
            Assert.Equal("Петр", firstName.name);
            Assert.Equal("Алексеевич", firstName.patronymic);
            Assert.Equal("Кузнецов", firstName.surname);

            Assert.IsType<Address>(cleaned[1]);
            var address = (Address)cleaned[1];
            Assert.Equal("77000000000717100", address.kladr_id);
            Assert.Equal("Сретенский бульвар", address.metro[0].name);
        }
    }
}

