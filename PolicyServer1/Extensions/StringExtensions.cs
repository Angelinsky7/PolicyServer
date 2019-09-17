using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Extensions {
    public static class StringExtensions {

        public static String ReturnNullIfEmpty(this String src) {
            return String.IsNullOrEmpty(src) ? null : src;
        }

    }
}
