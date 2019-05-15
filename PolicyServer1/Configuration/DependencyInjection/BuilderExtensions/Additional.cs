using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PolicyServer1.Stores;
using PolicyServer1.Stores.Caching;

namespace Microsoft.Extensions.DependencyInjection {
    public static class PolicyServerBuilderExtensionsAdditional {

        public static IPolicyServerBuilder AddClientStore<T>(this IPolicyServerBuilder builder) where T : class, IClientStore {
            builder.Services.TryAddTransient(typeof(T));
            builder.Services.AddTransient<IClientStore, ValidatingClientStore<T>>();

            //builder.Services.AddTransient<IclientStore, T>();

            return builder;
        }

        public static IPolicyServerBuilder AddPolicyStore<T>(this IPolicyServerBuilder builder) where T : class, IPolicyStore {
            builder.Services.TryAddTransient(typeof(T));
            builder.Services.AddTransient<IPolicyStore, ValidatingPolicyStore<T>>();

            return builder;
        }

        public static IPolicyServerBuilder AddPermissionStore<T>(this IPolicyServerBuilder builder) where T : class, IPermissionStore {
            builder.Services.TryAddTransient(typeof(T));
            builder.Services.AddTransient<IPermissionStore, ValidatingPermissionStore<T>>();

            return builder;
        }

        public static IPolicyServerBuilder AddRoleStore<T>(this IPolicyServerBuilder builder) where T : class, IRoleStore {
            builder.Services.TryAddTransient(typeof(T));
            builder.Services.AddTransient<IRoleStore, ValidatingRoleStore<T>>();

            return builder;
        }

        public static IPolicyServerBuilder AddClientStoreCache<T>(this IPolicyServerBuilder builder) where T : IClientStore {
            builder.Services.TryAddTransient(typeof(T));
            builder.Services.AddTransient<ValidatingClientStore<T>>();
            builder.Services.AddTransient<IClientStore, CachingClientStore<ValidatingClientStore<T>>>();

            return builder;
        }

        public static IPolicyServerBuilder AddPolicyStoreCache<T>(this IPolicyServerBuilder builder) where T : IPolicyStore {
            builder.Services.TryAddTransient(typeof(T));
            builder.Services.AddTransient<ValidatingPolicyStore<T>>();
            builder.Services.AddTransient<IPolicyStore, CachingPolicyStore<ValidatingPolicyStore<T>>>();

            return builder;
        }

        public static IPolicyServerBuilder AddPermissionStoreCache<T>(this IPolicyServerBuilder builder) where T : IPermissionStore {
            builder.Services.TryAddTransient(typeof(T));
            builder.Services.AddTransient<ValidatingPermissionStore<T>>();
            builder.Services.AddTransient<IPermissionStore, CachingPermissionStore<ValidatingPermissionStore<T>>>();

            return builder;
        }

        public static IPolicyServerBuilder AddRoleStoreCache<T>(this IPolicyServerBuilder builder) where T : IRoleStore {
            builder.Services.TryAddTransient(typeof(T));
            builder.Services.AddTransient<ValidatingRoleStore<T>>();
            builder.Services.AddTransient<IRoleStore, CachingRoleStore<ValidatingRoleStore<T>>>();

            return builder;
        }

    }
}
