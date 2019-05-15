using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PolicyServer1.Infrastructure {
    internal class ObjectSerializer {

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly JsonSerializer Serializer = new JsonSerializer {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
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
