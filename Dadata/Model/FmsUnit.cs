using System;
namespace Dadata.Model
{
    public class FmsUnit : IOutward
    {
        public string code { get; set; }
        public string name { get; set; }
        public string region_code { get; set; }
        public string type { get; set; }
    }
}
