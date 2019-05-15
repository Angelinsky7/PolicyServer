using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace PolicyServer1.Authorization {
    public class DefaultPolicyServerAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider {
        public DefaultPolicyServerAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options) { }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(String policyName) {
            AuthorizationPolicy policy = await base.GetPolicyAsync(policyName);

            if (policy == null) {
                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new DefaultPolicyServerPermissionRequirement(policyName))
                    .Build();
            }

            return policy;
        }

    }
}
