using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolicyServer1.EntityFramework.Storage.Datas;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Options;

namespace PolicyServer1.EntityFramework.Storage.Configuration {
    public static class PolicyServerEntityFrameworkBuilderExtensions {

        public static IServiceCollection AddPolicyDbContext(
            this IServiceCollection services,
            Action<clientStoreOptions> storeOptionsAction = null
        ) {
            return services.AddPolicyDbContext<PolicyDbContext>(storeOptionsAction);
        }

        public static IServiceCollection AddPolicyDbContext<TContext>(
            this IServiceCollection services,
            Action<clientStoreOptions> storeOptionsAction = null
        ) where TContext : DbContext, IPolicyDbContext {
            clientStoreOptions options = new clientStoreOptions();

            services.AddSingleton(options);
            storeOptionsAction?.Invoke(options);

            if (options.ResolveDbContextOptions != null) {
                services.AddDbContext<TContext>(options.ResolveDbContextOptions);
            } else {
                services.AddDbContext<TContext>(dbCtxBuilder => {
                    options.ConfigureDbContext?.Invoke(dbCtxBuilder);
                });
            }
            services.AddScoped<IPolicyDbContext, TContext>();

            return services;
        }

    }
}
