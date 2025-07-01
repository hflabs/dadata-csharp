using Newtonsoft.Json;

namespace Dadata.Model
{
    public class MktuRecord : IOutward
    {
        [JsonProperty("class")]
        public string klass { get; set; }
        public string number { get; set; }
        public string name_ru { get; set; }
        public string name_en { get; set; }
        public string name_fr { get; set; }
    }
}
