using System;
using System.Security.Claims;
using System.Threading.Tasks;
using PolicyServer1.Models;

namespace PolicyServer1.Services {
   
    public interface IPolicyService {
        
        Task<PolicyResult> EvaluateAsync(Policy policy, ClaimsPrincipal user);
        
        // Task<Boolean> HasPermissionAsync(ClaimsPrincipal user, String permission);
        // Task<Boolean> IsInRoleAsync(ClaimsPrincipal user, String role);
    }
}