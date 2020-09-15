using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPolicyServer.Extensions {
    public static class EnumExtensions {

        public static IEnumerable<T> GetAsList<T>() where T : Enum {
            return Enum.GetNames(typeof(T)).Select(p => (T)Enum.Parse(typeof(T), p));
        }

    }
}
