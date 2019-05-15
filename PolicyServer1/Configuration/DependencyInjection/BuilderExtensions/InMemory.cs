using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PolicyServer1.Models;
using PolicyServer1.Services;
using PolicyServer1.Services.Default;
using PolicyServer1.Stores.InMemory;

namespace Microsoft.Extensions.DependencyInjection {
    public static class PolicyServerBuilderExtensionsInMemory {

        public static IPolicyServerBuilder AddInMemoryPolicies(this IPolicyServerBuilder builder, IEnumerable<Client> clients) {
            builder.Services.AddSingleton(clients);

            builder.AddClientStore<InMemoryClientStore>();

            //var existingCors = builder.Services.Where(x => x.ServiceType == typeof(ICorsPolicyService)).LastOrDefault();
            //if (existingCors != null &&
            //    existingCors.ImplementationType == typeof(DefaultCorsPolicyService) &&
            //    existingCors.Lifetime == ServiceLifetime.Transient) {
            //    // if our default is registered, then overwrite with the InMemoryCorsPolicyService
            //    // otherwise don't overwrite with the InMemoryCorsPolicyService, which uses the custom one registered by the host
            //    builder.Services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();
            //}

            return builder;
        }


        /// <summary>
        /// Adds the in memory clients.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="section">The configuration section containing the configuration data.</param>
        /// <returns></returns>
        public static IPolicyServerBuilder AddInMemoryPolicies(this IPolicyServerBuilder builder, IConfigurationSection section) {
            List<Client> clients = new List<Client>();
            section.Bind(clients);

            return builder.AddInMemoryPolicies(clients);
        }

        public static IPolicyServerBuilder AddInMemoryCaching(this IPolicyServerBuilder builder) {
            builder.Services.TryAddSingleton<IMemoryCache, MemoryCache>();
            builder.Services.TryAddTransient(typeof(ICache<>), typeof(DefaultCache<>));

            return builder;
        }

    }
}
