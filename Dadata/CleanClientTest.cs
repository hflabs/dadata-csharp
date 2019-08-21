using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Dadata {

    [TestFixture]
    public class CleanClientTest {

        public CleanClient api { get; set; }

        [SetUp]
        public void SetUp() {
            //this.api = new CleanClient("REPLACE_WITH_YOUR_API_KEY", "REPLACE_WITH_YOUR_SECRET_KEY", "dadata.ru", "https");
            this.api = new CleanClient("b0ad5e1f0a72a2926377024494293f473db2fe08", "2a316e5f8814764e52343d8e05b1ec521bd8e236", "dadata.ru", "https");
        }

        [Test]
        public void CleanAsIsTest() {
            DoCleanGeneric<AsIsData>(new string[] { "Раз", "Два", "Три" });
        }

        [Test]
        public void CleanAddressTest() {
            DoCleanGeneric<AddressData>(new string[] { "Москва Милютинский 13", "Питер Восстания 1" });
        }

        [Test]
        public void CleanBirthdateTest() {
            DoCleanGeneric<BirthdateData>(new string[] { "12.03.1990", "25.12.1980" });
        }

        [Test]
        public void CleanEmailTest() {
            DoCleanGeneric<EmailData>(new string[] { "mr@matrix.net", "anderson@matrix.ru" });
        }

        [Test]
        public void CleanNameTest() {
            DoCleanGeneric<NameData>(new string[] { "Леша Вязов", "Ольга Викторовна Раздербань" });
        }

        [Test]
        public void CleanPhoneTest() {
            DoCleanGeneric<PhoneData>(new string[] { "495 245 23-34", "89168459285" });
        }
        
        [Test]
        public void CleanPassportTest() {
            DoCleanGeneric<PassportData>(new string[] { "4509 235857", "4506 629672" });
        }
        
        [Test]
        public void CleanVehicleTest() {
            DoCleanGeneric<VehicleData>(new string[] { "форд фокус", "citroen c3" });
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

            Assert.IsInstanceOf<NameData>(cleanedRecords[0][0], "Expected [0,0] entity to be a Name");
            var firstName = (NameData)cleanedRecords[0][0];
            Assert.AreEqual(firstName.name, "Петр", 
                String.Format("Expected name 'Петр', but got {0}", firstName.name));
            Assert.AreEqual(firstName.patronymic, "Алексеевич", 
                String.Format("Expected patronymic 'Алексеевич', but got {0}", firstName.patronymic));
            Assert.AreEqual(firstName.surname, "Кузнецов", 
                String.Format("Expected surname 'Кузнецов', but got {0}", firstName.surname));

            Assert.IsInstanceOf<AddressData>(cleanedRecords[0][1], "Expected [0,1] entity to be an Address");
            var firstAddress = (AddressData)cleanedRecords[0][1];
            Assert.AreEqual(firstAddress.kladr_id, "77000000000717100", 
                String.Format("Expected kladr id '77000000000717100', but got {0}", firstAddress.kladr_id));
            Assert.AreEqual(firstAddress.metro[0].name, "Сретенский бульвар");
        }

        private void DoCleanGeneric<T>(string[] inputs) where T : IDadataEntity  {
            var cleaned = api.Clean<T>(inputs);
            var idx = 0;
            foreach (T entity in cleaned) {
                Console.WriteLine(entity.ToString());
                Assert.That(entity.ToString().Contains(inputs[idx]), 
                    String.Format("Expected {0} to be in {1}", inputs[idx], cleaned));
                idx++;
            }
        }
    }
}

