using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPolicyServer.Quickstart {
    public static class StringExtensions {

        public static String RemoveSchema(this String src) {
            if (String.IsNullOrEmpty(src)) { return String.Empty; }
            Int32 indexOfDot = src.IndexOf(".");
            if (indexOfDot == -1) { return src; }
            return src.Substring(indexOfDot + 1);
        }

    }
}
