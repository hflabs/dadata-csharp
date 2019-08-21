using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Dadata.Model;

namespace Dadata
{

    /// <summary>
    /// DaData Suggestions API (https://dadata.ru/api/suggest/)
    /// </summary>
    public class SuggestClient : ClientBase
    {

        const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        public SuggestClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

        public SuggestResponse<Address> SuggestAddress(string query, int count = 5)
        {
            var request = new SuggestAddressRequest(query, count);
            return SuggestAddress(request);
        }

        public SuggestResponse<Address> FindByIdAddress(string query)
        {
            var request = new SuggestRequest(query);
            return Execute<SuggestResponse<Address>>(method: SuggestionsMethod.FindById, entity: SuggestionsEntity.Address, request: request);
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

        public SuggestResponse<Bank> FindByIdBank(string query)
        {
            var request = new SuggestRequest(query);
            return Execute<SuggestResponse<Bank>>(method: SuggestionsMethod.FindById, entity: SuggestionsEntity.Bank, request: request);
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

        public SuggestResponse<Party> FindByIdParty(string query)
        {
            var request = new FindByIdPartyRequest(query);
            return FindByIdParty(request);
        }

        public SuggestResponse<Party> FindByIdParty(FindByIdPartyRequest request)
        {
            return Execute<SuggestResponse<Party>>(method: SuggestionsMethod.FindById, entity: SuggestionsEntity.Party, request: request);
        }

        public SuggestResponse<Party> SuggestParty(SuggestPartyRequest request)
        {
            return Execute<SuggestResponse<Party>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Party, request: request);
        }
    }
}
