using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PolicyServer1.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Hosting {
    public class PolicyClientMiddleware {

        private readonly RequestDelegate _next;
        private readonly ILogger<PolicyClientMiddleware> _logger;

        public PolicyClientMiddleware(RequestDelegate next, ILogger<PolicyClientMiddleware> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IPolicyClientService client) {
            if (context.User.Identity.IsAuthenticated) {
                Models.PolicyResult policy = await client.GetPolicyAsync(context);

                if (policy.Roles != null && policy.Permissions != null) {
                    IEnumerable<Claim> roleClaims = policy.Roles.Select(x => new Claim(Constants.Policy.Role, x, nameof(String), client.Authority));
                    IEnumerable<Claim> permissionClaims = policy.Permissions.Select(x => new Claim(Constants.Policy.Permission, x, nameof(String), client.Authority));

                    ClaimsIdentity id = new ClaimsIdentity(Constants.Policy.Identity, "name", Constants.Policy.Role);
                    id.AddClaims(roleClaims);
                    id.AddClaims(permissionClaims);

                    context.User.AddIdentity(id);
                }
            }

            await _next(context);
        }

    }
}
