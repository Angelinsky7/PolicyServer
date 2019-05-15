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

        Task<PolicyResult> GetPolicyAsync(HttpContext context);
    }
}
