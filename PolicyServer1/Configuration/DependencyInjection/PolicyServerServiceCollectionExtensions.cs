using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PolicyServer1.Configuration;

namespace Microsoft.Extensions.DependencyInjection {
    public static class PolicyServerServiceCollectionExtensions {

        public static IPolicyServerBuilder AddPolicyServerBuilder(this IServiceCollection services) {
            return new PolicyServerBuilder(services);
        }

        public static IPolicyServerBuilder AddPolicyServer(this IServiceCollection services) {
            IPolicyServerBuilder builder = services.AddPolicyServerBuilder();

            builder
                .AddRequiredPlatformServices()
                .AddDefaultEndpoints()
                .AddValidators()
                .AddRequestParsers()
                .AddResponseGenerators()
                .AddServices();

            return builder;
        }

        public static IPolicyServerBuilder AddPolicyServer(this IServiceCollection services, Action<PolicyServerOptions> setupAction) {
            services.Configure(setupAction);
            return services.AddPolicyServer();
        }

    }

}
