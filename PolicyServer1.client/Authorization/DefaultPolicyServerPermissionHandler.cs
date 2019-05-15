using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PolicyServer1.Client.Services;

namespace PolicyServer1.Authorization {
    public class DefaultPolicyServerPermissionHandler : AuthorizationHandler<DefaultPolicyServerPermissionRequirement> {

        private readonly IPolicyClientService _client;

        public DefaultPolicyServerPermissionHandler(IPolicyClientService client) {
            _client = client;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DefaultPolicyServerPermissionRequirement requirement) {
            if (await _client.HasPermissionAsync(context.User, requirement.Name)) {
                context.Succeed(requirement);
            }
        }
    }
}
