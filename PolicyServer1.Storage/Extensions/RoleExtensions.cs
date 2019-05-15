using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Models {

    public static class RoleExtensions {

        public static List<Permission> GetPermissions(this Role role, List<Role> rolesVisited) {
            List<Permission> result = new List<Permission>();

            foreach (Role parentRole in role.Parents) {
                if (!rolesVisited.Any(p => p.Name == parentRole.Name)) {
                    rolesVisited.Add(parentRole);
                    result.AddRange(GetPermissions(parentRole, rolesVisited));
                }
            }

            result.RemoveRevokedPermission();
            result.AddRange(role.Permissions);

            //TODO(demarco): maybe just @ the end
            //if (result.HasDuplicates(p => p.Name)) {
            //    //TODO(demarco): how to clean ?
            //}

            return result;
        }

        public static Boolean Evaluate(this Role role, ClaimsPrincipal user) {
            if (user == null) throw new ArgumentNullException(nameof(user));

            //TODO(demarco): how do we remove this and replace with an option ?
            String sub = user.FindFirst("sub")?.Value;
            if (!String.IsNullOrWhiteSpace(sub)) {
                if (role.Subjects.Contains(sub)) { return true; }
            }

            IEnumerable<String> roles = user.FindAll("role").Select(x => x.Value);
            if (roles.Any()) {
                if (role.IdentityRoles.Any(x => roles.Contains(x))) { return true; }
            }

            return false;
        }


    }

}