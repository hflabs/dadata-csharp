using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Dadata.Model
{
    public class DateRuConverter : JsonConverter
    {
        CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }
            return DateTime.Parse(reader.Value.ToString(), culture);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
