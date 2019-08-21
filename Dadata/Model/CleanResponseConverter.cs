using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Dadata.Model;

namespace Dadata.Model {

    /// <summary>
    /// Custom deserializer for IDadataEntity concrete types.
    /// </summary>
    internal class CleanResponseConverter : CustomCreationConverter<IDadataEntity> {

        public override IDadataEntity Create(Type objectType) {
            throw new NotImplementedException();
        }

        public IDadataEntity Create(Type objectType, JObject jObject) {
            StructureType type = GuessType(jObject);
            return CreateEntity(type);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        /// <summary>
        /// Guesse structure type by JSON object received as a clean result.
        /// </summary>
        /// <returns>Guessed structure type.</returns>
        /// <param name="jObject">JSON object.</param>
        private StructureType GuessType(JObject jObject) {
            if (jObject.Property("kladr_id") != null) {
                return StructureType.ADDRESS;
            } else if (jObject.Property("birthdate") != null) {
                return StructureType.BIRTHDATE;
            } else if (jObject.Property("email") != null) {
                return StructureType.EMAIL;
            } else if (jObject.Property("surname") != null) {
                return StructureType.NAME;
            } else if (jObject.Property("series") != null) {
                return StructureType.PASSPORT;
            } else if (jObject.Property("phone") != null) {
                return StructureType.PHONE;
            } else if (jObject.Property("brand") != null) {
                return StructureType.VEHICLE;
            } else {
                return StructureType.AS_IS;
            }
        }

        /// <summary>
        /// Create entity based on its' structure type.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="type">Structure type.</param>
        private IDadataEntity CreateEntity(StructureType type) {
            switch (type) {
            case StructureType.ADDRESS:
                return new Address();
            case StructureType.AS_IS:
                return new AsIs();
            case StructureType.BIRTHDATE:
                return new Birthdate();
            case StructureType.EMAIL:
                return new Email();
            case StructureType.NAME:
                return new Fullname();
            case StructureType.PASSPORT:
                return new Passport();
            case StructureType.PHONE:
                return new Phone();
            case StructureType.VEHICLE:
                return new Vehicle();
            default:
                return new AsIs();
            }
        }
    }

}
