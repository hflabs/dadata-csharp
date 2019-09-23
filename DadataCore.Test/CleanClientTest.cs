using System;
using System.Collections.Generic;
using NUnit.Framework;
using DadataCore.Model;

namespace DadataCore.Test {

    [TestFixture]
    public class CleanClientTest {

        public CleanClient api { get; set; }

        [SetUp]
        public void SetUp() {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var secret = Environment.GetEnvironmentVariable("DADATA_SECRET_KEY");
            api = new CleanClient(token, secret);
        }

        [Test]
        public void CleanAsIsTest() {
            var cleaned = api.Clean<AsIs>("Омномном");
            Assert.AreEqual(cleaned.source, "Омномном");
        }

        [Test]
        public void CleanAddressTest() {
            var cleaned = api.Clean<Address>("Москва Милютинский 13");
            Assert.AreEqual(cleaned.street, "Милютинский");
            Assert.AreEqual(cleaned.qc, "0");
        }

        [Test]
        public void CleanBirthdateTest() {
            var cleaned = api.Clean<Birthdate>("12.03.1990");
            Assert.AreEqual(cleaned.birthdate, new DateTime(1990, 3, 12));
            Assert.AreEqual(cleaned.qc, "0");
        }

        [Test]
        public void CleanEmailTest() {
            var cleaned = api.Clean<Email>("anderson@matrix.ru");
            Assert.AreEqual(cleaned.email, "anderson@matrix.ru");
            Assert.AreEqual(cleaned.qc, "0");

        }

        [Test]
        public void CleanNameTest() {
            var cleaned = api.Clean<Fullname>("Ольга Викторовна Раздербань");
            Assert.AreEqual(cleaned.name, "Ольга");
            Assert.AreEqual(cleaned.qc, "0");
        }

        [Test]
        public void CleanPhoneTest() {
            var cleaned = api.Clean<Phone>("89168459285");
            Assert.AreEqual(cleaned.number, "8459285");
            Assert.AreEqual(cleaned.qc, "0");
        }
        
        [Test]
        public void CleanPassportTest() {
            var cleaned = api.Clean<Passport>("4506 629672");
            Assert.AreEqual(cleaned.series, "45 06");
            Assert.AreEqual(cleaned.qc, "10");
        }
        
        [Test]
        public void CleanVehicleTest() {
            var cleaned = api.Clean<Vehicle>("форд фокус");
            Assert.AreEqual(cleaned.brand, "FORD");
            Assert.AreEqual(cleaned.qc, "0");
        }

        [Test]
        public void CleanTest() {
            var structure = new List<StructureType> { StructureType.NAME, StructureType.ADDRESS };
            var data = new List<string> { "Кузнецов Петр Алексеич", "Москва Милютинский 13" };

            var cleaned = api.Clean(structure, data);
            Assert.AreEqual(cleaned.Count, 2);

            Assert.IsInstanceOf<Fullname>(cleaned[0], "Expected [0] entity to be a Fullname");
            var firstName = (Fullname)cleaned[0];
            Assert.AreEqual(firstName.name, "Петр");
            Assert.AreEqual(firstName.patronymic, "Алексеевич");
            Assert.AreEqual(firstName.surname, "Кузнецов");

            Assert.IsInstanceOf<Address>(cleaned[1], "Expected [1] entity to be an Address");
            var address = (Address)cleaned[1];
            Assert.AreEqual(address.kladr_id, "77000000000717100");
            Assert.AreEqual(address.metro[0].name, "Сретенский бульвар");
        }
    }
}

