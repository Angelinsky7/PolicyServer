using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace PolicyServer1.Extensions {
    public static class IReadableStringCollectionExtensions {

        [DebuggerStepThrough]
        public static NameValueCollection AsNameValueCollection(this IEnumerable<KeyValuePair<String, StringValues>> collection) {
            NameValueCollection nv = new NameValueCollection();

            foreach (KeyValuePair<String, StringValues> field in collection) {
                foreach (String value in field.Value) {
                    nv.Add(field.Key, value);
                }
                //nv.Add(field.Key, field.Value.First());
            }

            return nv;
        }

    }
}
