using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Dadata.Model;

namespace Dadata
{
    public class GeolocateClient
    {
        const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        string token;
        string baseUrl;
        JsonSerializer serializer;

        static GeolocateClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        }

        public GeolocateClient(string token, string baseUrl = BASE_URL)
        {
            this.token = token;
            this.baseUrl = baseUrl;
            this.serializer = new JsonSerializer();
        }

        public SuggestResponse<Address> Geolocate(double lat, double lon)
        {
            var request = new GeolocateRequest(lat, lon);
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
                return JsonConvert.DeserializeObject<SuggestResponse<Address>>(responseText);
            }
        }

        private HttpWebRequest CreateHttpRequest()
        {
            var url = String.Format("{0}/geolocate/address", baseUrl);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Token " + this.token);
            return request;
        }
    }
}
