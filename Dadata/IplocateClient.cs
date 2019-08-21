using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Dadata.Model;

namespace Dadata
{
    public class IplocateClient
    {
        const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        string token;
        string baseUrl;
        JsonSerializer serializer;

        static IplocateClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        }

        public IplocateClient(string token, string baseUrl = BASE_URL)
        {
            this.token = token;
            this.baseUrl = baseUrl;
            this.serializer = new JsonSerializer();
        }

        public IplocateResponse Iplocate(string ip)
        {
            var httpRequest = CreateHttpRequest(ip);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var r = new StreamReader(httpResponse.GetResponseStream()))
            {
                string responseText = r.ReadToEnd();
                return JsonConvert.DeserializeObject<IplocateResponse>(responseText);
            }
        }

        private HttpWebRequest CreateHttpRequest(string ip)
        {
            var url = String.Format("{0}/iplocate/address?ip={1}", baseUrl, ip);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Token " + this.token);
            return request;
        }
    }
}
