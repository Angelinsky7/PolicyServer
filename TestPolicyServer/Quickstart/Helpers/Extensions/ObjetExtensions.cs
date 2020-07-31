using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestPolicyServer.Quickstart {
    public static class ObjetExtensions {

        public static String ToJson(this Object src) {
            return JsonConvert.SerializeObject(src, Formatting.None);
        }

    }
}
