using System;
using Microsoft.EntityFrameworkCore;
using PolicyServer1.EntityFramework.Storage.Configuration;
using PolicyServer1.EntityFramework.Storage.Datas;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Options;
using PolicyServer1.EntityFramework.Storage.Stores;

namespace Microsoft.Extensions.DependencyInjection {
    public static class PolicyServerEntityFrameworkBuilderExtensions {

        public static IPolicyServerBuilder AddConfigurationStore(
            this IPolicyServerBuilder builder,
            Action<clientStoreOptions> storeOptionsAction = null
        ) {
            return builder.AddConfigurationStore<PolicyDbContext>(storeOptionsAction);
        }

        public static IPolicyServerBuilder AddConfigurationStore<TContext>(
            this IPolicyServerBuilder builder,
            Action<clientStoreOptions> storeOptionsAction = null
        ) where TContext : DbContext, IPolicyDbContext {

            builder.Services.AddPolicyDbContext<TContext>(storeOptionsAction);

            builder.AddClientStore<ClientStore>();
            builder.AddPolicyStore<PolicyStore>();
            builder.AddPermissionStore<PermissionStore>();
            builder.AddRoleStore<RoleStore>();


            return builder;
        }

        public static IPolicyServerBuilder AddConfigurationStoreCache(
            this IPolicyServerBuilder builder
        ) {
            builder.AddInMemoryCaching();
            builder.AddClientStoreCache<ClientStore>();
            builder.AddPolicyStoreCache<PolicyStore>();
            builder.AddPermissionStoreCache<PermissionStore>();
            builder.AddRoleStoreCache<RoleStore>();
           
            return builder;
        }

    }
}
