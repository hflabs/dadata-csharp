using System;
using System.Collections.Generic;
using NUnit.Framework;
using Dadata.Model;

namespace Dadata.Test {

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
            var structure = new List<StructureType>(
                new StructureType[] { StructureType.NAME, StructureType.ADDRESS }
            );

            var data = new List<List<string>>(new List<string>[] {
                new List<string>(new string[] { "Кузнецов Петр Алексеич", "Москва Милютинский 13" }),
                new List<string>(new string[] { "Марципанова Ольга Викторовна", null }),
                new List<string>(new string[] { "Пузин Витя", null })
            });

            var request = new CleanRequest(structure, data);
            var cleanedRecords = api.Clean(request).data;
            Assert.AreEqual(cleanedRecords.Count, 3, 
                String.Format("Expected 3 records, but got {0}", cleanedRecords.Count));

            Assert.IsInstanceOf<Fullname>(cleanedRecords[0][0], "Expected [0,0] entity to be a Name");
            var firstName = (Fullname)cleanedRecords[0][0];
            Assert.AreEqual(firstName.name, "Петр", 
                String.Format("Expected name 'Петр', but got {0}", firstName.name));
            Assert.AreEqual(firstName.patronymic, "Алексеевич", 
                String.Format("Expected patronymic 'Алексеевич', but got {0}", firstName.patronymic));
            Assert.AreEqual(firstName.surname, "Кузнецов", 
                String.Format("Expected surname 'Кузнецов', but got {0}", firstName.surname));

            Assert.IsInstanceOf<Address>(cleanedRecords[0][1], "Expected [0,1] entity to be an Address");
            var firstAddress = (Address)cleanedRecords[0][1];
            Assert.AreEqual(firstAddress.kladr_id, "77000000000717100", 
                String.Format("Expected kladr id '77000000000717100', but got {0}", firstAddress.kladr_id));
            Assert.AreEqual(firstAddress.metro[0].name, "Сретенский бульвар");
        }
    }
}

