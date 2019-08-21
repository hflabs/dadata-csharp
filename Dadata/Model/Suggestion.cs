using System;
namespace Dadata.Model
{
    public class Suggestion<T>
    {
        public string value { get; set; }
        public string unrestricted_value { get; set; }
        public T data { get; set; }
    }
}
