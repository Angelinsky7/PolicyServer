using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PolicyServer1.Configuration;
using PolicyServer1.Extensions;
using PolicyServer1.Models;
using PolicyServer1.Services;

namespace PolicyServer1.Stores.Caching {
    public class CachingRoleStore<T> : IRoleStore where T : IRoleStore {

        private readonly T _inner;
        private readonly PolicyServerOptions _options;
        private readonly ICache<PolicyRole> _cache;
        private readonly ILogger _logger;

        public CachingRoleStore(
            PolicyServerOptions options,
            T inner,
            ICache<PolicyRole> cache,
            ILogger<CachingRoleStore<T>> logger
        ) {
            _options = options;
            _inner = inner;
            _cache = cache;
            _logger = logger;
        }

        public Task<Int32> CreateAsync(PolicyRole newRole) {
            throw new NotImplementedException();
        }

        public async Task<PolicyRole> GetAsync((Int32 policyId, Int32 roleId) key) {
            PolicyRole Role = await _cache.GetAsync(
                key.ToString(),
                _options.Caching.RoleStoreExpiration,
                () => _inner.GetAsync(key),
                _logger
            );

            return Role;
        }

        public Task RemoveAsync((Int32 policyId, Int32 roleId) key) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync((Int32 policyId, Int32 roleId) key, PolicyRole Role) {
            throw new NotImplementedException();
        }
    }
}
