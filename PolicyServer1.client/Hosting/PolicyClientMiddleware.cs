using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PolicyServer1.Client.Configuration.Options;
using PolicyServer1.Client.Services;
using PolicyServer1.Models.Evalutation;
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
        private readonly PolicyClientOptions _options;

        public PolicyClientMiddleware(RequestDelegate next, ILogger<PolicyClientMiddleware> logger, PolicyClientOptions options) {
            _next = next;
            _logger = logger;
            _options = options;
        }

        public async Task Invoke(HttpContext context, IPolicyClientService client) {
            if (context.User.Identity.IsAuthenticated) {
                //TODO(demarco): Rework this part please !

                PermissionResult result = await client.GetPermissionsAsync(context);

                //var clientId = context.User.FindFirst("client_id")?.Value;

                //TODO(demarco): I'm really not sure about that... i think i want to have a differece between URL and issuer !!!
                if (result.ClientId == _options.ClientId &&
                    result.Issuer == _options.Authority &&
                    result.Results != null
                ) {
                    IEnumerable<Claim> permissionClaims = result.Results.SelectMany(p => p.Scopes.Select(a => new Claim(Constants.Policy.Permission, $"{p.RsName}{_options.PermissionSplitter}{a}", nameof(String), client.Authority)));

                    ClaimsIdentity id = new ClaimsIdentity(Constants.Policy.Identity, "name", Constants.Policy.Role);
                    //id.AddClaims(roleClaims);
                    id.AddClaims(permissionClaims);

                    context.User.AddIdentity(id);
                }
                //TODO(demarco): Rework this part please !
            }

            await _next(context);
        }

    }
}
