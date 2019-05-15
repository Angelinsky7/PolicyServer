using PolicyServer1.Extensions;
using PolicyServer1.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Services.Default {
    /// <summary>
    /// PolicyServer1 client
    /// </summary>
    public class DefaultPolicyService : IPolicyService {

        public DefaultPolicyService() {
        }

        // public async Task<Boolean> IsInRoleAsync(ClaimsPrincipal user, String role) {
        //     PolicyResult policy = await EvaluateAsync(user);
        //     return policy.Roles.Contains(role);
        // }

        // public async Task<Boolean> HasPermissionAsync(ClaimsPrincipal user, String permission) {
        //     PolicyResult policy = await EvaluateAsync(user);
        //     return policy.Permissions.Contains(permission);
        // }

        public async Task<PolicyResult> EvaluateAsync(Policy policy, ClaimsPrincipal user) {
            if (policy.Hash.IsMissing()) { policy.CalculateHash(); }
            if (policy.LastPolicyChangeDate == DateTime.MinValue) { policy.LastPolicyChangeDate = DateTime.Now; }
            PolicyResult result = await policy.EvaluateAsync(user);
            return result;
        }
    }
}