using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Models {

    public static class PermissionExtensions {

        public static void RemoveRevokedPermission(this List<Permission> src) {
            src.RemoveAll(p => src.Where(a => a.IsRevoked).Select(a => a.Name).Contains(p.Name));
        }

        public static void CleanPermission(this List<Permission> src) {
            src.RemoveAll(p => p.IsRevoked);
        }

    }

}