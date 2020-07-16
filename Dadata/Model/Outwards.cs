using System;
using System.Collections.Generic;

namespace Dadata.Model
{
    public class Outwards
    {
        static Dictionary<Type, string> TYPE_TO_ENTITY = new Dictionary<Type, string>() {
            { typeof(CarBrand), SuggestionsEntity.CarBrand },
            { typeof(DeliveryCity), SuggestionsEntity.Delivery },
            { typeof(FmsUnit), SuggestionsEntity.FmsUnit },
            { typeof(MetroStation), SuggestionsEntity.Metro },
            { typeof(PostalUnit), SuggestionsEntity.PostalUnit },
        };

        public static string GetEntityName(Type type)
        {
            return TYPE_TO_ENTITY[type];
        }
    }
}
