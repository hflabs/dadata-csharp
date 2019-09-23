using DadataCore.Model;
using System.Threading.Tasks;

namespace DadataCore {

	/// <summary>
	/// DaData Suggestions API (https://dadata.ru/api/suggest/)
	/// </summary>
	public class SuggestClient : ClientBase {

		const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

		public SuggestClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

		public async Task<SuggestResponse<Address>> SuggestAddress(string query, int count = 5) {
			var request = new SuggestAddressRequest(query, count);
			return await SuggestAddress(request);
		}

		public async Task<SuggestResponse<Address>> FindAddress(string query) {
			var request = new SuggestRequest(query);
			return await Execute<SuggestResponse<Address>>(
				method: SuggestionsMethod.Find,
				entity: SuggestionsEntity.Address, request: request);
		}

		public async Task<SuggestResponse<Address>> SuggestAddress(SuggestAddressRequest request) {
			return await Execute<SuggestResponse<Address>>(
				method: SuggestionsMethod.Suggest,
				entity: SuggestionsEntity.Address, request: request);
		}

		public async Task<SuggestResponse<Bank>> SuggestBank(string query, int count = 5) {
			var request = new SuggestBankRequest(query, count);
			return await SuggestBank(request);
		}

		public async Task<SuggestResponse<Bank>> FindBank(string query) {
			var request = new SuggestRequest(query);
			return await Execute<SuggestResponse<Bank>>(
				method: SuggestionsMethod.Find,
				entity: SuggestionsEntity.Bank, request: request);
		}

		public async Task<SuggestResponse<Bank>> SuggestBank(SuggestBankRequest request) {
			return await Execute<SuggestResponse<Bank>>(
				method: SuggestionsMethod.Suggest,
				entity: SuggestionsEntity.Bank, request: request);
		}

		public async Task<SuggestResponse<Email>> SuggestEmail(string query, int count = 5) {
			var request = new SuggestRequest(query, count);
			return await SuggestEmail(request);
		}

		public async Task<SuggestResponse<Email>> SuggestEmail(SuggestRequest request) {
			return await Execute<SuggestResponse<Email>>(
				method: SuggestionsMethod.Suggest,
				entity: SuggestionsEntity.Email, request: request);
		}

		public async Task<SuggestResponse<Fullname>> SuggestName(string query, int count = 5) {
			var request = new SuggestNameRequest(query, count);
			return await SuggestName(request);
		}

		public async Task<SuggestResponse<Fullname>> SuggestName(SuggestNameRequest request) {
			return await Execute<SuggestResponse<Fullname>>(
				method: SuggestionsMethod.Suggest,
				entity: SuggestionsEntity.Name, request: request);
		}

		public async Task<SuggestResponse<Party>> SuggestParty(string query, int count = 5) {
			var request = new SuggestPartyRequest(query, count);
			return await SuggestParty(request);
		}

		public async Task<SuggestResponse<Party>> FindParty(string query) {
			var request = new FindPartyRequest(query);
			return await FindParty(request);
		}

		public async Task<SuggestResponse<Party>> FindParty(FindPartyRequest request) {
			return await Execute<SuggestResponse<Party>>(
				method: SuggestionsMethod.Find,
				entity: SuggestionsEntity.Party, request: request);
		}

		public async Task<SuggestResponse<Party>> SuggestParty(SuggestPartyRequest request) {
			return await
				Execute<SuggestResponse<Party>>(
					method: SuggestionsMethod.Suggest,
					entity: SuggestionsEntity.Party, request: request);
		}
	}
}
