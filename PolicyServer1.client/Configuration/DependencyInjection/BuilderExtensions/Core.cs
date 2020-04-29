using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PolicyServer1.Authorization;
using PolicyServer1.Client.Configuration.Options;
using PolicyServer1.Client.Services;
using PolicyServer1.Client.Services.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection {
    public static class PolicyClientBuilderExtensionsCore {

        public static IPolicyClientBuilder AddRequiredPlatformServices(this IPolicyClientBuilder builder) {
            builder.Services.AddOptions();
            builder.Services.AddSingleton(p => p.GetRequiredService<IOptions<PolicyClientOptions>>().Value);

            return builder;
        }

        public static IPolicyClientBuilder AddClientServices(this IPolicyClientBuilder builder) {
            builder.Services.AddTransient<IPolicyClientService, DefaultPolicyClientService>();

            return builder;
        }

        public static IPolicyClientBuilder AddLocalServices(this IPolicyClientBuilder builder, Action<LocalServiceOption> optionsAction) {
            LocalServiceOption options = new LocalServiceOption();

            builder.Services.AddSingleton(options);
            optionsAction?.Invoke(options);

            builder.Services.AddTransient<IPolicyClientService, DefaultPolicyLocalService>();

            return builder;
        }

        public static IPolicyClientBuilder AddAuthorizationPermissionPolicies(this IPolicyClientBuilder builder) {
            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddTransient<IAuthorizationPolicyProvider, DefaultPolicyServerAuthorizationPolicyProvider>();
            builder.Services.AddTransient<IAuthorizationHandler, DefaultPolicyServerPermissionHandler>();

            return builder;
        }

    }
}
