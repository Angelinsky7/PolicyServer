using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PolicyServer1.Extensions {
    public static class StringExtensions {

        public static String ReturnNullIfEmpty(this String src) {
            return String.IsNullOrEmpty(src) ? null : src;
        }

        public static Regex WildcardToRegex(this String src) {
            String pattern = Regex.Escape(src)
                .Replace(@"\*", ".*")
                .Replace(@"\?", ".");

            return new Regex($"^{pattern}$");
        }

    }
}
