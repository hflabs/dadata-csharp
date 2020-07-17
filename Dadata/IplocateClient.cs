using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Dadata.Model;

namespace Dadata
{
    [Obsolete("Use SuggestClient instead")]
    public class IplocateClient : ClientBaseSync
    {
        const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        public IplocateClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

        public IplocateResponse Iplocate(string ip)
        {
            var parameters = new NameValueCollection();
            parameters["ip"] = ip;
            return ExecuteGet<IplocateResponse>(method: "iplocate", entity: "address", parameters: parameters);
        }
    }
}
