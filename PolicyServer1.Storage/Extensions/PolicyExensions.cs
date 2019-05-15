using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Models {

    public static class PolicyExtensions {

        public static Task<PolicyResult> EvaluateAsync(this Policy policy, ClaimsPrincipal user) {
            if (user == null) throw new ArgumentNullException(nameof(user));

            List<Role> rolesVisited = new List<Role>();

            List<Permission> permissions = new List<Permission>();
            foreach (Role role in policy.Roles.Where(p => p.Evaluate(user))) {
                if (!rolesVisited.Any(p => p.Name == role.Name)) {
                    rolesVisited.Add(role);
                    //TODO(demarco): need to clean dupicate permissions at same layer and revokation
                    permissions.AddRange(role.GetPermissions(rolesVisited));
                }
            }

            permissions.RemoveRevokedPermission();
            permissions.CleanPermission();

            PolicyResult result = new PolicyResult() {
                PolicyHash = policy.Hash,
                LastPolicyChangeDate = policy.LastPolicyChangeDate,
                Roles = rolesVisited.Select(p => p.Name).Distinct(),
                Permissions = permissions.Select(p => p.Name).Distinct()
            };
            
            return Task.FromResult(result);
        }

    }

}