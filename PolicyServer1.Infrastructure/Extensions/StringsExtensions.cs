using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PolicyServer1.Extensions {
    internal static class StringsExtensions {

        [DebuggerStepThrough]
        public static Boolean IsPresent(this String value) {
            return !String.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static Boolean IsMissing(this String value) {
            return String.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static String EnsureLeadingSlash(this String url) {
            if (!url.StartsWith("/")) { return "/" + url; }

            return url;
        }

        [DebuggerStepThrough]
        public static String EnsureTrailingSlash(this String url) {
            if (!url.EndsWith("/")) { return url + "/"; }

            return url;
        }

        [DebuggerStepThrough]
        public static String RemoveLeadingSlash(this String url) {
            if (url != null && url.StartsWith("/")) {
                url = url.Substring(1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static String RemoveTrailingSlash(this String url) {
            if (url != null && url.EndsWith("/")) {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static String CleanUrlPath(this String url) {
            if (String.IsNullOrWhiteSpace(url)) url = "/";

            if (url != "/" && url.EndsWith("/")) {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

    }
}
