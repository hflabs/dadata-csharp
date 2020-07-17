using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Dadata.Model;

namespace Dadata
{
    public abstract class ClientBaseSync : ClientBase
    {
        public ClientBaseSync(string token, string baseUrl) : this(token, null, baseUrl) { }

        public ClientBaseSync(string token, string secret, string baseUrl) : base(token, secret, baseUrl) { }

        protected T ExecuteGet<T>(string method, string entity)
        {
            var parameters = new NameValueCollection();
            return ExecuteGet<T>(method, entity, parameters);
        }

        protected T ExecuteGet<T>(string method, string entity, NameValueCollection parameters)
        {
            var queryString = SerializeParameters(parameters);
            var url = BuildUrl(method: method, entity: entity, queryString: queryString);
            var httpRequest = CreateHttpRequest(verb: "GET", url: url);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            return Deserialize<T>(httpResponse);
        }

        protected T Execute<T>(string method, string entity, IDadataRequest request)
        {
            var url = BuildUrl(method: method, entity: entity);
            var httpRequest = CreateHttpRequest(verb: "POST", url: url);
            httpRequest = Serialize(httpRequest, request);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            return Deserialize<T>(httpResponse);
        }

        protected HttpWebRequest CreateHttpRequest(string verb, string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = verb;
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Token " + this.token);
            if (this.secret != null)
            {
                request.Headers.Add("X-Secret", this.secret);
            }
            return request;
        }

        protected HttpWebRequest Serialize(HttpWebRequest httpRequest, IDadataRequest request)
        {
            using (var w = new StreamWriter(httpRequest.GetRequestStream()))
            using (JsonWriter writer = new JsonTextWriter(w))
            {
                this.serializer.Serialize(writer, request);
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
