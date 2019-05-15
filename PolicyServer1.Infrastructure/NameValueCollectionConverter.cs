using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace PolicyServer1.Infrastructure {
    internal class NameValueCollectionConverter : JsonConverter {
        public class NameValueCollectionItem {
            public String Key { get; set; }
            public String[] Values { get; set; }
        }

        public override Boolean CanConvert(Type objectType) {
            return objectType == typeof(NameValueCollection);
        }

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer) {
            if (!(existingValue is NameValueCollection collection)) collection = new NameValueCollection();

            NameValueCollectionItem[] items = serializer.Deserialize<NameValueCollectionItem[]>(reader);
            if (items != null) {
                foreach (NameValueCollectionItem item in items) {
                    if (item.Values != null) {
                        foreach (String value in item.Values) {
                            collection.Add(item.Key, value);
                        }
                    }
                }
            }

            return collection;
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer) {
            if (value is NameValueCollection collection) {
                NameValueCollectionItem[] items = new NameValueCollectionItem[collection.AllKeys.Length];
                Int32 index = 0;
                foreach (String key in collection.AllKeys) {
                    items[index++] = new NameValueCollectionItem {
                        Key = key,
                        Values = collection.GetValues(key)
                    };
                }

                serializer.Serialize(writer, items);
            }
        }
    }
}
