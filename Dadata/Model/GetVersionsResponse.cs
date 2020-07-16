using System;
using System.Collections.Generic;

namespace Dadata.Model
{
    public class GetVersionsResponse : IDadataResponse
    {
        public ProductVersion dadata { get; set; }
        public ProductVersion suggestions { get; set; }
        public ProductVersion factor { get; set; }
    }

    public class ProductVersion
    {
        public string version { get; set; }
        public IDictionary<string, string> resources { get; set; }
    }
}
