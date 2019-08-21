using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dadata {

    /// <summary>
    /// Interacts with DaData clean API (https://dadata.ru/api/clean/)
    /// </summary>
    public class CleanClient {
    
        const string CLEAN_URL = "{0}://{1}/api/v2/clean";

        string token;
        string secret;
        string url;
        CustomCreationConverter<IDadataEntity> converter;
        JsonSerializer serializer;

        // maps concrete IDadataEntity types to corresponding structure types
        static Dictionary<Type, StructureType> TYPE_TO_STRUCTURE = new Dictionary<Type, StructureType>() {
            { typeof(AddressData),      StructureType.ADDRESS },
            { typeof(AsIsData),         StructureType.AS_IS },
            { typeof(BirthdateData),    StructureType.BIRTHDATE },
            { typeof(EmailData),        StructureType.EMAIL },
            { typeof(NameData),         StructureType.NAME },
            { typeof(PassportData),     StructureType.PASSPORT },
            { typeof(PhoneData),        StructureType.PHONE },
            { typeof(VehicleData),      StructureType.VEHICLE }
        };

        static CleanClient() {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        }

        /// <summary>
        /// Creates an instance to interact with DaData clean API (https://dadata.ru/api/clean/).
        /// </summary>
        /// <param name="token">API key.</param>
        /// <param name="hostname">DaData server hostname.</param>
        /// <param name="protocol">HTTP protocol (http or https, defaut http).</param>
        public CleanClient(string token, string hostname, string protocol = "http") : 
            this(token, null, hostname, protocol) {
        }
            
        /// <summary>
        /// Creates an instance to interact with DaData clean API (https://dadata.ru/api/clean/).
        /// </summary>
        /// <param name="token">API key.</param>
        /// <param name="secret">API secret.</param>
        /// <param name="hostname">DaData server hostname.</param>
        /// <param name="protocol">HTTP protocol (http or https, defaut http).</param>
        public CleanClient(string token, string secret, string hostname, string protocol = "http") {
            this.token = token;
            this.secret = secret;
            this.url = String.Format(CLEAN_URL, protocol, hostname);
            // all response data entities look the same (IDadataEntity), 
            // need to manually convert them to specific types (address, phone etc)
            this.converter = new CleanResponseConverter();
            this.serializer = new JsonSerializer();
            // need to serialize StructureType as string, not int
            serializer.Converters.Add(new StringEnumConverter());
        }

        /// <summary>
        /// Clean records via DaData.ru.
        /// </summary>
        /// <param name="request">Clean request.</param>
        public CleanResponse Clean(CleanRequest request) {
            var httpRequest = CreateHttpRequest();

            // prepare serialized json request
            using (var w = new StreamWriter(httpRequest.GetRequestStream())) {
                using (JsonWriter writer = new JsonTextWriter(w)) {
                    this.serializer.Serialize(writer, request);
                }
            }

            // get response and de-serialize it to typed records
            var httpResponse = (HttpWebResponse) httpRequest.GetResponse();
            using (var r = new StreamReader(httpResponse.GetResponseStream())) {
                string responseText = r.ReadToEnd();
                return JsonConvert.DeserializeObject<CleanResponse>(responseText, this.converter);
            }
        }

        /// <summary>
        /// Clean entities of specified type via DaData.ru.
        /// </summary>
        /// <param name="inputs">Input data as array of raw strings (addresses, phones etc).</param>
        /// <typeparam name="T">Target entity type as supported by DaData (IDadataEntity subtypes — AddressData, PhoneData etc).</typeparam>
        public IList<T> Clean<T>(IEnumerable<string> inputs) where T : IDadataEntity {
            // infer structure from target entity type
            var structure = new List<StructureType>(
                new StructureType[] { TYPE_TO_STRUCTURE[typeof(T)] }
            );
            // transform enity list to CleanRequest data structure
            var data = new List<List<string>>();
            foreach (string input in inputs) {
                data.Add(new List<string>(new string[] { input }));
            }
            var request = new CleanRequest(structure, data);
            // get response and transform it to list of entities
            var response = Clean(request);
            var outputs = new List<T>();
            foreach (IList<IDadataEntity> row in response.data) {
                outputs.Add((T)row[0]);
            }
            return outputs;
        }

        /// <summary>
        /// Create DaData HTTP request with necessary defaults.
        /// </summary>
        /// <returns>The http request.</returns>
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
