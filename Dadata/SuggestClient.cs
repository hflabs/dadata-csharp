using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Dadata.Model;

namespace Dadata
{

    /// <summary>
    /// DaData Suggestions API (https://dadata.ru/api/suggest/)
    /// </summary>
    public class SuggestClient
    {

        const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        string token;
        string baseUrl;
        JsonSerializer serializer;

        static SuggestClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        }

        public SuggestClient(string token, string baseUrl = BASE_URL)
        {
            this.token = token;
            this.baseUrl = baseUrl;
            this.serializer = new JsonSerializer();
        }

        public SuggestResponse<Address> SuggestAddress(string query, int count = 5)
        {
            var request = new SuggestAddressRequest(query, count);
            return SuggestAddress(request);
        }

        public SuggestResponse<Address> SuggestAddress(SuggestAddressRequest request)
        {
            return Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Address, request: request);
        }

        public SuggestResponse<Bank> SuggestBank(string query, int count = 5)
        {
            var request = new SuggestBankRequest(query, count);
            return SuggestBank(request);
        }

        public SuggestResponse<Bank> SuggestBank(SuggestBankRequest request)
        {
            return Execute<SuggestResponse<Bank>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Bank, request: request);
        }

        public SuggestResponse<Email> SuggestEmail(string query, int count = 5)
        {
            var request = new SuggestRequest(query, count);
            return SuggestEmail(request);
        }

        public SuggestResponse<Email> SuggestEmail(SuggestRequest request)
        {
            return Execute<SuggestResponse<Email>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Email, request: request);
        }

        public SuggestResponse<Fullname> SuggestName(string query, int count = 5)
        {
            var request = new SuggestNameRequest(query, count);
            return SuggestName(request);
        }

        public SuggestResponse<Fullname> SuggestName(SuggestNameRequest request)
        {
            return Execute<SuggestResponse<Fullname>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Name, request: request);
        }

        public SuggestResponse<Party> SuggestParty(string query, int count = 5)
        {
            var request = new SuggestPartyRequest(query, count);
            return SuggestParty(request);
        }

        public SuggestResponse<Party> SuggestParty(SuggestPartyRequest request)
        {
            return Execute<SuggestResponse<Party>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Party, request: request);
        }

        private T Execute<T>(string method, string entity, SuggestRequest request)
        {
            var httpRequest = CreateHttpRequest(method: method, entity: entity);

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
                return JsonConvert.DeserializeObject<T>(responseText);
            }
        }

        private HttpWebRequest CreateHttpRequest(string method, string entity)
        {
            var url = String.Format("{0}/{1}/{2}", baseUrl, method, entity);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Token " + this.token);
            return request;
        }

    }
}
