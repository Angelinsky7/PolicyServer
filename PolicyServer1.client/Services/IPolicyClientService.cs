using Microsoft.AspNetCore.Http;
using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Client.Services {
    public interface IPolicyClientService {
        String Authority { get; }

        Task<Boolean> HasPermissionAsync(ClaimsPrincipal user, String permissionName);
        Task<Boolean> HasRoleAsync(ClaimsPrincipal user, String roleName);

        //TODO(demarco): We need to change everything
        //Task<PolicyResult> GetPolicyAsync(HttpContext context);

        //Task<EvaluationResult> GetPermissionsAsync(HttpContext context);
        //Task<EvaluationTest> GetDecisionAsync(HttpContext context);
    }
}
