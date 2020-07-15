using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Dadata.Model;

namespace Dadata
{
    public abstract class ClientBase
    {
        protected string token;
        protected string baseUrl;
        protected JsonSerializer serializer;

        static ClientBase()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public ClientBase(string token, string baseUrl)
        {
            this.token = token;
            this.baseUrl = baseUrl;
            this.serializer = new JsonSerializer();
        }

        protected T ExecuteGet<T>(string method, string entity, NameValueCollection parameters)
        {
            var queryString = SerializeParameters(parameters);
            var httpRequest = CreateHttpRequest(verb: "GET", method: method, entity: entity, queryString: queryString);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            return Deserialize<T>(httpResponse);
        }

        protected T Execute<T>(string method, string entity, IDadataRequest request)
        {
            var httpRequest = CreateHttpRequest(verb: "POST", method: method, entity: entity);
            httpRequest = Serialize(httpRequest, request);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            return Deserialize<T>(httpResponse);
        }

        protected T Execute<T>(IDadataRequest request)
        {
            var httpRequest = CreateHttpRequest(verb: "POST", url: baseUrl);
            httpRequest = Serialize(httpRequest, request);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            return Deserialize<T>(httpResponse);
        }

        protected HttpWebRequest CreateHttpRequest(string verb, string method, string entity, string queryString = null)
        {
            var url = String.Format("{0}/{1}/{2}", baseUrl, method, entity);
            if (queryString != null)
            {
                url += "?" + queryString;
            }
            return CreateHttpRequest(verb, url);
        }

        protected HttpWebRequest CreateHttpRequest(string verb, string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = verb;
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Token " + this.token);
            return request;
        }

        protected string SerializeParameters(NameValueCollection parameters)
        {
            List<string> parts = new List<string>();
            foreach (String key in parameters.AllKeys)
                parts.Add(String.Format("{0}={1}", key, parameters[key]));
            return String.Join("&", parts);
        }

        protected HttpWebRequest Serialize(HttpWebRequest httpRequest, IDadataRequest request)
        {
            using (var w = new StreamWriter(httpRequest.GetRequestStream()))
            {
                using (JsonWriter writer = new JsonTextWriter(w))
                {
                    this.serializer.Serialize(writer, request);
                }
            }
            return httpRequest;
        }

        protected virtual T Deserialize<T>(HttpWebResponse httpResponse)
        {
            using (var r = new StreamReader(httpResponse.GetResponseStream()))
            {
                string responseText = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(responseText);
            }
        }
    }
}
