using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace PolicyServer1.Infrastructure {
    internal class ObjectSerializer {

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            DefaultValueHandling = DefaultValueHandling.Populate,
            NullValueHandling = NullValueHandling.Include,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private static readonly JsonSerializer Serializer = new JsonSerializer {
            DefaultValueHandling = DefaultValueHandling.Populate,
            NullValueHandling = NullValueHandling.Include,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        static ObjectSerializer() {
            Settings.Converters.Add(new NameValueCollectionConverter());
        }

        public static String ToString(Object o) {
            return JsonConvert.SerializeObject(o, Settings);
        }

        public static T FromString<T>(String value) {
            return JsonConvert.DeserializeObject<T>(value, Settings);
        }
        
        public static JObject ToJObject(Object o) {
            return JObject.FromObject(o, Serializer);
        }

    }
}
