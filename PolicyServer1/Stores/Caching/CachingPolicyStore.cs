using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PolicyServer1.Configuration;
using PolicyServer1.Extensions;
using PolicyServer1.Models;
using PolicyServer1.Services;

namespace PolicyServer1.Stores.Caching {
    public class CachingPolicyStore<T> : IPolicyStore where T : IPolicyStore {

        private readonly T _inner;
        private readonly PolicyServerOptions _options;
        private readonly ICache<Policy> _cache;
        private readonly ILogger _logger;

        public CachingPolicyStore(
            PolicyServerOptions options,
            T inner,
            ICache<Policy> cache,
            ILogger<CachingPolicyStore<T>> logger
        ) {
            _options = options;
            _inner = inner;
            _cache = cache;
            _logger = logger;
        }

        public Task<Policy> CreateAsync(Policy item) {
            throw new NotImplementedException();
        }

        public Task<Policy> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public IQueryable<Policy> Get() {
            throw new NotImplementedException();
        }

        public Task<Policy> RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Policy> UpdateAsync(Guid id, Policy item) {
            throw new NotImplementedException();
        }

        //public Task<Int32> CreateAsync(Policy newPolicy) {
        //    throw new NotImplementedException();
        //}

        //public async Task<Policy> GetAsync(Int32 policyId) {
        //    Policy policy = await _cache.GetAsync(
        //        policyId.ToString(),
        //        _options.Caching.PolicyStoreExpiration,
        //        () => _inner.GetAsync(policyId),
        //        _logger
        //    );

        //    return policy;
        //}

        //public Task RemoveAsync(Int32 policyId) {
        //    throw new NotImplementedException();
        //}

        //public Task UpdateAsync(Int32 policyId, Policy policy) {
        //    throw new NotImplementedException();
        //}
    }
}
