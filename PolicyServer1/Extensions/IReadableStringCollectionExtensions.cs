using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace PolicyServer1.Extensions {
    public static class IReadableStringCollectionExtensions {

        [DebuggerStepThrough]
        public static NameValueCollection AsNameValueCollection(this IEnumerable<KeyValuePair<string, StringValues>> collection) {
            var nv = new NameValueCollection();

            foreach (var field in collection) {
                nv.Add(field.Key, field.Value.First());
            }

            return nv;
        }

    }
}
