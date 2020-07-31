using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using PolicyServer1.Configuration;
using PolicyServer1.Endpoints;
using PolicyServer1.Extensions;
using PolicyServer1.Hosting;
using PolicyServer1.Models;
using PolicyServer1.ResponseHandling;
using PolicyServer1.ResponseHandling.Default;
using PolicyServer1.Services;
using PolicyServer1.Services.Default;
using PolicyServer1.Validations;
using PolicyServer1.Validations.Default;
using static PolicyServer1.Constants;

namespace Microsoft.Extensions.DependencyInjection {
    public static class PolicyServerBuilderExtensionsCore {

        public static IPolicyServerBuilder AddRequiredPlatformServices(this IPolicyServerBuilder builder) {
            builder.Services.AddOptions();
            builder.Services.AddSingleton(p => p.GetRequiredService<IOptions<PolicyServerOptions>>().Value);

            return builder;
        }

        public static IPolicyServerBuilder AddValidators(this IPolicyServerBuilder builder) {
            builder.Services.TryAddTransient<IPolicyConfigurationValidator, DefaultPolicyConfigurationValidator>();
            builder.Services.TryAddTransient<IClientConfigurationValidator, DefaultClientConfigurationValidator>();
            builder.Services.TryAddTransient<IPermissionConfigurationValidator, DefaultPermissionConfigurationValidator>();
            builder.Services.TryAddTransient<IRoleConfigurationValidator, DefaultRoleConfigurationValidator>();

            builder.Services.TryAddTransient<IScopeConfigurationValidator, DefaultScopeConfigurationValidator>();
            builder.Services.TryAddTransient<IResourceConfigurationValidator, DefaultResourceConfigurationValidator>();

            return builder;
        }

        public static IPolicyServerBuilder AddDefaultEndpoints(this IPolicyServerBuilder builder) {
            builder.Services.AddTransient<IEndpointRouter, EndpointRouter>();

            //builder.AddEndpoint<PolicyEndpoint>(EndpointNames.Policy, ProtocolRoutePaths.Policy.EnsureLeadingSlash());
            builder.AddEndpoint<PermissionEndpoint>(EndpointNames.Permission, ProtocolRoutePaths.Permission.EnsureLeadingSlash());
            builder.AddEndpoint<DiscoveryEndpoint>(EndpointNames.Discovery, ProtocolRoutePaths.DiscoveryConfiguration.EnsureLeadingSlash());

            return builder;
        }

        public static IPolicyServerBuilder AddRequestParsers(this IPolicyServerBuilder builder) {
            //builder.Services.AddTransient<IPolicyRequestParser, DefaultPolicyRequestParser>();
            builder.Services.AddTransient<IPermissionRequestParser, DefaultPermissionRequestParser>();

            return builder;
        }

        public static IPolicyServerBuilder AddResponseGenerators(this IPolicyServerBuilder builder) {
            //builder.Services.AddTransient<IPolicyResponseGenerator, DefaultPolicyResponseGenerator>();
            builder.Services.AddTransient<IPermissionResponseGenerator, DefaultPermissionResponseGenerator>();
            builder.Services.AddTransient<IDiscoveryResponseGenerator, DefaultDiscoveryResponseGenerator>();

            return builder;
        }

        public static IPolicyServerBuilder AddEndpoint<T>(this IPolicyServerBuilder builder, String name, PathString path) where T : class, IEndpointHandler {
            builder.Services.AddTransient<T>();
            builder.Services.AddSingleton(new Endpoint(name, path, typeof(T)));

            return builder;
        }

        public static IPolicyServerBuilder AddServices(this IPolicyServerBuilder builder) {
            //builder.Services.AddTransient<IPolicyService, DefaultPolicyService>();
            builder.Services.AddTransient<IEvaluatorService, DefaultEvaluatorService>();

            return builder;
        }

    }
}
