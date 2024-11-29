using System;
using System.Threading.Tasks;
using System.Text.Json;
using Xunit;
using Dadata.Model;

namespace Dadata.Test
{
    public class SerializeTest
    {
        public SuggestClientAsync api { get; set; }

        public SerializeTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            api = new SuggestClientAsync(token);
        }

        [Fact]
        public async Task SerializeResponseTest()
        {
            var response = await api.FindParty("7707083893");
            var json = JsonSerializer.Serialize(response.suggestions);
            Assert.NotEqual("{}", json);
        }
    }
}
