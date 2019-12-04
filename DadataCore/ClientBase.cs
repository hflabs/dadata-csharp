using DadataCore.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DadataCore {
	public abstract class ClientBase {
		protected string token;
		protected string baseUrl;
		protected JsonSerializer serializer;

		static ClientBase() {
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
		}

		public ClientBase(string token, string baseUrl) {
			this.token = token;
			this.baseUrl = baseUrl;
			this.serializer = new JsonSerializer();
		}

		protected async Task<T> ExecuteGet<T>(string method, string entity, NameValueCollection parameters) {
			var queryString = SerializeParameters(parameters);
			var httpRequest = CreateHttpRequest(verb: "GET", method: method, entity: entity, queryString: queryString);
			var httpResponse = await httpRequest.GetResponseAsync();
			return await Deserialize<T>((HttpWebResponse)httpResponse);
		}

		protected async Task<T> Execute<T>(string method, string entity, IDadataRequest request) {
			var httpRequest = CreateHttpRequest(verb: "POST", method: method, entity: entity);
			httpRequest = Serialize(httpRequest, request);
			var httpResponse = await httpRequest.GetResponseAsync();
			return await Deserialize<T>((HttpWebResponse)httpResponse);
		}

		protected async Task<T> Execute<T>(IDadataRequest request) {
			var httpRequest = CreateHttpRequest(verb: "POST", url: baseUrl);
			httpRequest = Serialize(httpRequest, request);
			var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();
			return await Deserialize<T>(httpResponse);
		}

		protected HttpWebRequest CreateHttpRequest(string verb, string method, string entity, string queryString = null) {
			var url = String.Format("{0}/{1}/{2}", baseUrl, method, entity);
			if (queryString != null) {
				url += "?" + queryString;
			}
			return CreateHttpRequest(verb, url);
		}

		protected HttpWebRequest CreateHttpRequest(string verb, string url) {
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = verb;
			request.ContentType = "application/json";
			request.Headers.Add("Authorization", "Token " + this.token);
			return request;
		}

		protected string SerializeParameters(NameValueCollection parameters) {
			List<string> parts = new List<string>();
			foreach (String key in parameters.AllKeys)
				parts.Add(String.Format("{0}={1}", key, parameters[key]));
			return String.Join("&", parts);
		}

		protected HttpWebRequest Serialize(HttpWebRequest httpRequest, IDadataRequest request) {
			using (var w = new StreamWriter(httpRequest.GetRequestStream())) {
				using (JsonWriter writer = new JsonTextWriter(w)) {
					this.serializer.Serialize(writer, request);
				}
			}
			return httpRequest;
		}

		protected virtual async Task<T> Deserialize<T>(HttpWebResponse httpResponse) {
			using (var r = new StreamReader(httpResponse.GetResponseStream())) {
				string responseText = await r.ReadToEndAsync();
				return JsonConvert.DeserializeObject<T>(responseText);
			}
		}
	}
}
