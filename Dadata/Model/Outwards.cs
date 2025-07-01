using System;
using System.Collections.Generic;

namespace Dadata.Model
{
    public class Outwards
    {
        static Dictionary<Type, string> TYPE_TO_ENTITY = new Dictionary<Type, string>() {
            { typeof(CarBrand), SuggestionsEntity.CarBrand },
            { typeof(Country), SuggestionsEntity.Country },
            { typeof(Currency), SuggestionsEntity.Currency},
            { typeof(DeliveryCity), SuggestionsEntity.Delivery },
            { typeof(FmsUnit), SuggestionsEntity.FmsUnit },
            { typeof(FnsUnit), SuggestionsEntity.FnsUnit },
            { typeof(FtsUnit), SuggestionsEntity.FtsUnit },
            { typeof(MetroStation), SuggestionsEntity.Metro },
            { typeof(MktuRecord), SuggestionsEntity.Mktu },
            { typeof(OkpdRecord), SuggestionsEntity.Okpd2},
            { typeof(OktmoRecord), SuggestionsEntity.Oktmo},
            { typeof(OkvedRecord), SuggestionsEntity.Okved2},
            { typeof(PostalUnit), SuggestionsEntity.PostalUnit },
        };

        public static string GetEntityName(Type type)
        {
            return TYPE_TO_ENTITY[type];
        }
    }
}
