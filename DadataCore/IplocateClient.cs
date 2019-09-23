using DadataCore.Model;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace DadataCore {
	public class IplocateClient : ClientBase {
		const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

		public IplocateClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

		public async Task<IplocateResponse> Iplocate(string ip) {
			var parameters = new NameValueCollection();
			parameters["ip"] = ip;
			return await ExecuteGet<IplocateResponse>(
				method: "iplocate",
				entity: "address", parameters: parameters
				);
		}
	}
}
