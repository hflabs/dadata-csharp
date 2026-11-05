using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    // Changes global Newtonsoft settings, so must not run in parallel with other tests
    [CollectionDefinition("GlobalSettings", DisableParallelization = true)]
    public class GlobalSettingsCollection { }

    [Collection("GlobalSettings")]
    public class GlobalSettingsTest
    {
        const string RESPONSE = @"{""suggestions"": [{
            ""value"": ""ПАО СБЕРБАНК"",
            ""unrestricted_value"": ""ПАО СБЕРБАНК"",
            ""unknown_field"": 42,
            ""data"": {""inn"": ""7707083893"", ""unknown_field"": 42}
        }]}";

        class TestClient : SuggestClientAsync
        {
            public TestClient() : base("token") { }

            public Task<SuggestResponse<Party>> Parse(string json)
            {
                var response = new HttpResponseMessage
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                return Deserialize<SuggestResponse<Party>>(response);
            }
        }

        [Fact]
        public async Task GlobalSettingsTestAll()
        {
            var original = JsonConvert.DefaultSettings;
            Func<JsonSerializerSettings> settings = () => new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            JsonConvert.DefaultSettings = settings;
            try
            {
                var client = new TestClient();
                // client does not override global settings
                Assert.Same(settings, JsonConvert.DefaultSettings);
                // client does not use global settings to parse responses
                var response = await client.Parse(RESPONSE);
                Assert.Equal("ПАО СБЕРБАНК", response.suggestions[0].value);
                Assert.Equal("7707083893", response.suggestions[0].data.inn);
            }
            finally
            {
                JsonConvert.DefaultSettings = original;
            }
        }
    }
}
