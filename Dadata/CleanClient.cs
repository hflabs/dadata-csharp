using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Dadata.Model;

namespace Dadata {

    /// <summary>
    /// DaData Clean API (https://dadata.ru/api/clean/)
    /// </summary>
    public class CleanClient {
    
        const string BASE_URL= "https://dadata.ru/api/v2/clean";

        string token;
        string secret;
        string url;
        CustomCreationConverter<IDadataEntity> converter;
        JsonSerializer serializer;

        // maps concrete IDadataEntity types to corresponding structure types
        static Dictionary<Type, StructureType> TYPE_TO_STRUCTURE = new Dictionary<Type, StructureType>() {
            { typeof(Address), StructureType.ADDRESS },
            { typeof(AsIs), StructureType.AS_IS },
            { typeof(Birthdate), StructureType.BIRTHDATE },
            { typeof(Email), StructureType.EMAIL },
            { typeof(Fullname), StructureType.NAME },
            { typeof(Passport), StructureType.PASSPORT },
            { typeof(Phone), StructureType.PHONE },
            { typeof(Vehicle), StructureType.VEHICLE }
        };

        static CleanClient() {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        }

        public CleanClient(string token, string secret, string baseUrl=BASE_URL) {
            this.token = token;
            this.secret = secret;
            this.url = baseUrl;
            // all response data entities look the same (IDadataEntity), 
            // need to manually convert them to specific types (address, phone etc)
            this.converter = new CleanResponseConverter();
            this.serializer = new JsonSerializer();
            // need to serialize StructureType as string, not int
            serializer.Converters.Add(new StringEnumConverter());
        }

        public T Clean<T>(string source) where T : IDadataEntity {
            // infer structure from target entity type
            var structure = new List<StructureType>(
                new StructureType[] { TYPE_TO_STRUCTURE[typeof(T)] }
            );
            // transform enity list to CleanRequest data structure
            var data = new string[] { source };
            var response = Clean(structure, data);
            return (T)response[0];
        }

        public IList<IDadataEntity> Clean(IEnumerable<StructureType> structure, IEnumerable<string> data)
        {
            var request = new CleanRequest(structure, data);
            var httpRequest = CreateHttpRequest();

            // prepare serialized json request
            using (var w = new StreamWriter(httpRequest.GetRequestStream()))
            {
                using (JsonWriter writer = new JsonTextWriter(w))
                {
                    this.serializer.Serialize(writer, request);
                }
            }

            // get response and de-serialize it to typed records
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var r = new StreamReader(httpResponse.GetResponseStream()))
            {
                string responseText = r.ReadToEnd();
                var response = JsonConvert.DeserializeObject<CleanResponse>(responseText, this.converter);
                return response.data[0];
            }
        }

        private HttpWebRequest CreateHttpRequest() {
            var request = (HttpWebRequest) WebRequest.Create(this.url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Token " + this.token);
            if (this.secret != null) {
                request.Headers.Add("X-Secret", this.secret);
            }
            return request;
        }

    }
}
