using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class CleanClientSyncTest
    {
        public CleanClientSync api { get; set; }

        public CleanClientSyncTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var secret = Environment.GetEnvironmentVariable("DADATA_SECRET_KEY");
            api = new CleanClientSync(token, secret);
        }

        [Fact]
        public void CleanAsIsTest()
        {
            var cleaned = api.Clean<AsIs>("Омномном");
            Assert.Equal("Омномном", cleaned.source);
        }

        [Fact]
        public void CleanAddressTest()
        {
            var cleaned = api.Clean<Address>("Москва Милютинский 13");
            Assert.Equal("Милютинский", cleaned.street);
            Assert.Equal("0", cleaned.qc);
        }

        [Fact]
        public void CleanBirthdateTest()
        {
            var cleaned = api.Clean<Birthdate>("12.03.1990");
            Assert.Equal(new DateTime(1990, 3, 12), cleaned.birthdate);
            Assert.Equal("0", cleaned.qc);
        }

        [Fact]
        public void CleanEmailTest()
        {
            var cleaned = api.Clean<Email>("anderson@matrix.ru");
            Assert.Equal("anderson@matrix.ru", cleaned.email);
            Assert.Equal("CORPORATE", cleaned.type);
            Assert.Equal("0", cleaned.qc);

        }

        [Fact]
        public void CleanNameTest()
        {
            var cleaned = api.Clean<Fullname>("Ольга Викторовна Раздербань");
            Assert.Equal("Ольга", cleaned.name);
            Assert.Equal("0", cleaned.qc);
        }

        [Fact]
        public void CleanPhoneTest()
        {
            var cleaned = api.Clean<Phone>("89168459285");
            Assert.Equal("8459285", cleaned.number);
            Assert.Equal("0", cleaned.qc);
        }

        [Fact]
        public void CleanPassportTest()
        {
            var cleaned = api.Clean<Passport>("4506 629672");
            Assert.Equal("45 06", cleaned.series);
            Assert.Equal("10", cleaned.qc);
        }

        [Fact]
        public void CleanTest()
        {
            var structure = new[] { StructureType.NAME, StructureType.ADDRESS };
            var data = new[] { "Кузнецов Петр Алексеич", "Москва Милютинский 13" };

            var cleaned = api.Clean(structure, data);
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

