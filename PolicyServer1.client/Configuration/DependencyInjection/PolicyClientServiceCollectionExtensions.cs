using PolicyServer1.Client.Configuration.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection {
    public static class PolicyClientServiceCollectionExtensions {

        public static IPolicyClientBuilder AddPolicyClientBuilder(this IServiceCollection services) {
            return new PolicyClientBuilder(services);
        }

        public static IPolicyClientBuilder AddPolicyClient(this IServiceCollection services) {
            IPolicyClientBuilder builder = services.AddPolicyClientBuilder();

            builder
                .AddRequiredPlatformServices()
                .AddAuthorizationPermissionPolicies();

            return builder;
        }

        public static IPolicyClientBuilder AddPolicyClient(this IServiceCollection services, Action<PolicyClientOptions> setupAction) {
            services.Configure(setupAction);
            return services.AddPolicyClient();
        }

    }
}
