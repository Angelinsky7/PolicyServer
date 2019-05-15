using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PolicyServer1.Extensions {
    public static class IEnumerableExtensions {

        [DebuggerStepThrough]
        public static Boolean IsNullOrEmpty<T>(this IEnumerable<T> list) {
            if (list == null) { return true; }
            if (!list.Any()) { return true; }

            return false;
        }

        public static Boolean HasDuplicates<T, TProp>(this IEnumerable<T> list, Func<T, TProp> selector) {
            HashSet<TProp> d = new HashSet<TProp>();
            foreach (T t in list) {
                if (!d.Add(selector(t))) {
                    return true;
                }
            }
            return false;
        }

    }
}
