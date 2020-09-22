using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Extensions {
    public static class ObjectExtensions {

        public static void UpdateIfDifferent<T>(T src, T target, Action<T> update) {
            if (!Object.Equals(src, target)) { update(target); }
        }

    }
}
