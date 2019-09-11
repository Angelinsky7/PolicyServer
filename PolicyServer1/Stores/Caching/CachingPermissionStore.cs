using Microsoft.Extensions.Logging;
using PolicyServer1.Configuration;
using PolicyServer1.Extensions;
using PolicyServer1.Models;
using PolicyServer1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores.Caching {
    public class CachingPermissionStore<T> : IPermissionStore where T : IPermissionStore {

        //private readonly T _inner;
        //private readonly PolicyServerOptions _options;
        //private readonly ICache<PolicyPermission> _cache;
        //private readonly ILogger _logger;

        //public CachingPermissionStore(
        //    PolicyServerOptions options,
        //    T inner,
        //    ICache<PolicyPermission> cache,
        //    ILogger<CachingPermissionStore<T>> logger
        //) {
        //    _options = options;
        //    _inner = inner;
        //    _cache = cache;
        //    _logger = logger;
        //}

        public Task<Permission> CreateAsync(Permission item) {
            throw new NotImplementedException();
        }

        public Task<Permission> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public IQueryable<Permission> Get() {
            throw new NotImplementedException();
        }

        public Task<Permission> GetByNameAsync(String name) {
            throw new NotImplementedException();
        }

        public Task<Permission> RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Permission> UpdateAsync(Guid id, Permission item) {
            throw new NotImplementedException();
        }

        //public Task<Int32> CreateAsync(PolicyPermission newPermission) {
        //    throw new NotImplementedException();
        //}

        //public async Task<PolicyPermission> GetAsync((Int32 policyId, Int32 permissionId) key) {
        //    PolicyPermission permission = await _cache.GetAsync(
        //        key.ToString(),
        //        _options.Caching.PermissionStoreExpiration,
        //        () => _inner.GetAsync(key),
        //        _logger
        //    );

        //    return permission;
        //}

        //public async Task<PolicyPermission> GetByNameAsync(Int32 policyId, String permissionName) {
        //    PolicyPermission permission = await _cache.GetAsync(
        //        policyId + permissionName,
        //        _options.Caching.PermissionStoreExpiration,
        //        () => _inner.GetByNameAsync(policyId, permissionName),
        //        _logger
        //    );

        //    return permission;
        //}

        //public Task RemoveAsync((Int32 policyId, Int32 permissionId) key) {
        //    throw new NotImplementedException();
        //}

        //public Task UpdateAsync((Int32 policyId, Int32 permissionId) key, PolicyPermission permission) {
        //    throw new NotImplementedException();
        //}
    }
}
