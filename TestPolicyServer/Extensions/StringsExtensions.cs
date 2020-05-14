using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TestPolicyServer.Extensions {
    public static class StringsExtensions {

        [DebuggerStepThrough]
        public static bool IsPresent(this string value) {
            return !string.IsNullOrWhiteSpace(value);
        }

    }
}
