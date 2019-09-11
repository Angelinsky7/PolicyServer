using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class ResouceScopeResult {

        public Permission Permission { get; set; }

        public Resource Resource { get; set; }
        public Scope Scope { get; set; }

        internal Int32 GrantedCount { get; set; }
        internal Int32 DeniedCount { get; set; }

        public Nullable<Boolean> Granted { get; set; }

        public static Int64 GetHash(Permission permission, Resource resource, Scope scope) {
            return 1610612741 * permission.Id.GetHashCode() + 805306457 * resource.Id.GetHashCode() + 402653189 * scope.Id.GetHashCode();
        }

    }
}
