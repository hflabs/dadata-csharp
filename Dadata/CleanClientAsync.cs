using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Dadata.Model;

namespace Dadata
{
    public class CleanClientAsync : ClientBaseAsync
    {
        protected const string BASE_URL = "https://cleaner.dadata.ru/api/v1";

        CustomCreationConverter<IDadataEntity> converter;

        // maps concrete IDadataEntity types to corresponding structure types
        static Dictionary<Type, StructureType> TYPE_TO_STRUCTURE = new Dictionary<Type, StructureType>()
        {
            { typeof(Address), StructureType.ADDRESS },
            { typeof(AsIs), StructureType.AS_IS },
            { typeof(Birthdate), StructureType.BIRTHDATE },
            { typeof(Email), StructureType.EMAIL },
            { typeof(Fullname), StructureType.NAME },
            { typeof(Passport), StructureType.PASSPORT },
            { typeof(Phone), StructureType.PHONE },
            { typeof(Vehicle), StructureType.VEHICLE }
        };


        public CleanClientAsync(string token, string secret, string baseUrl = BASE_URL) : base(token, secret, baseUrl)
        {
            // all response data entities look the same (IDadataEntity), 
            // need to manually convert them to specific types (address, phone etc)
            this.converter = new CleanResponseConverter();
            // need to serialize StructureType as string, not int
            serializer.Converters.Add(new StringEnumConverter());
        }

        public async Task<T> Clean<T>(string source) where T : IDadataEntity
        {
            // infer structure from target entity type
            var structure = new List<StructureType>(
                new StructureType[] { TYPE_TO_STRUCTURE[typeof(T)] }
            );
            // transform enity list to CleanRequest data structure
            var data = new string[] { source };
            var response = await Clean(structure, data);
            return (T)response[0];
        }

        public async Task<IList<IDadataEntity>> Clean(IEnumerable<StructureType> structure, IEnumerable<string> data)
        {
            var request = new CleanRequest(structure, data);
            var response = await Execute<CleanResponse>(method: "clean", entity: null, request: request);
            return response.data[0];
        }

        protected override async Task<T> Deserialize<T>(HttpResponseMessage httpResponse)
        {
            var stream = await httpResponse.Content.ReadAsStreamAsync();
            using (var r = new StreamReader(stream))
            {
                string responseText = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(responseText, this.converter);
            }
        }
    }
}
