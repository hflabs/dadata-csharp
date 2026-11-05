using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Dadata
{
    public abstract class ClientBase
    {
        protected string token;
        protected string secret;
        protected string baseUrl;
        protected JsonSerializer serializer;

        public ClientBase(string token, string baseUrl) : this(token, null, baseUrl) { }

        public ClientBase(string token, string secret, string baseUrl)
        {
            this.token = token;
            this.secret = secret;
            this.baseUrl = baseUrl;
            // JsonSerializer.Create ignores JsonConvert.DefaultSettings,
            // so the client neither depends on nor changes the global Newtonsoft settings
            this.serializer = JsonSerializer.Create();
        }

        protected T DeserializeText<T>(string text)
        {
            using (var r = new StringReader(text))
            using (var reader = new JsonTextReader(r))
            {
                return this.serializer.Deserialize<T>(reader);
            }
        }

        protected string BuildUrl(string method, string entity, string queryString = null)
        {
            var url = String.Format("{0}/{1}", baseUrl, method);
            if (!String.IsNullOrEmpty(entity))
            {
                url = String.Format("{0}/{1}", url, entity);
            }
            if (queryString != null)
            {
                url += "?" + queryString;
            }
            return url;
        }

        protected string SerializeParameters(NameValueCollection parameters)
        {
            List<string> parts = new List<string>();
            foreach (String key in parameters.AllKeys)
                parts.Add(String.Format("{0}={1}", key, parameters[key]));
            return String.Join("&", parts);
        }
    }
}
