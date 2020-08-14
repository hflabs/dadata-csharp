using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dadata.Model;

namespace Dadata
{
    public abstract class ClientBaseAsync : ClientBase
    {
        protected HttpClient client;

        public ClientBaseAsync(string token, string baseUrl, HttpClient client)
            : this(token, null, baseUrl, client) { }

        public ClientBaseAsync(string token, string secret, string baseUrl, HttpClient client)
            : base(token, secret, baseUrl)
        {
            this.client = client ?? new HttpClient();
        }

        protected async Task<T> ExecuteGet<T>(string method, string entity)
        {
            var parameters = new NameValueCollection();
            return await ExecuteGet<T>(method, entity, parameters);
        }

        protected async Task<T> ExecuteGet<T>(string method, string entity, NameValueCollection parameters)
        {
            var queryString = SerializeParameters(parameters);
            var url = BuildUrl(method: method, entity: entity, queryString: queryString);
            using (var httpRequest = CreateHttpRequest(verb: HttpMethod.Get, url: url))
            using (var httpResponse = await client.SendAsync(httpRequest))
            {
                httpResponse.EnsureSuccessStatusCode();
                return await Deserialize<T>(httpResponse);
            }
        }

        protected async Task<T> Execute<T>(string method, string entity, IDadataRequest request)
        {
            var url = BuildUrl(method: method, entity: entity);
            using (var httpRequest = CreateHttpRequest(verb: HttpMethod.Post, url: url))
            using (var httpContent = Serialize(httpRequest, request))
            {
                httpRequest.Content = httpContent;
                using (var httpResponse = await client.SendAsync(httpRequest))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    return await Deserialize<T>(httpResponse);
                }
            }
        }

        protected HttpRequestMessage CreateHttpRequest(HttpMethod verb, string url)
        {
            var request = new HttpRequestMessage(verb, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Token", this.token);
            if (this.secret != null)
            {
                request.Headers.Add("X-Secret", this.secret);
            }
            return request;
        }

        protected HttpContent Serialize(HttpRequestMessage httpRequest, IDadataRequest request)
        {
            MemoryStream stream = new MemoryStream();
            using (var w = new StreamWriter(stream, encoding: new UTF8Encoding(false), bufferSize: 1024, leaveOpen: true))
            using (JsonWriter writer = new JsonTextWriter(w))
            {
                this.serializer.Serialize(writer, request);
                writer.Flush();
            }
            stream.Seek(0, SeekOrigin.Begin);
            var httpContent = new StreamContent(stream);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpContent;
        }

        protected virtual async Task<T> Deserialize<T>(HttpResponseMessage httpResponse)
        {
            var stream = await httpResponse.Content.ReadAsStreamAsync();
            using (var r = new StreamReader(stream))
            {
                string responseText = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(responseText);
            }
        }
    }
}
