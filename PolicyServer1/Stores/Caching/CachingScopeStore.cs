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
    public class CachingScopeStore<T> : IScopeStore where T : IScopeStore {

        private readonly T _inner;
        private readonly PolicyServerOptions _options;
        private readonly ICache<Scope> _cache;
        private readonly ILogger _logger;

        public CachingScopeStore(
            PolicyServerOptions options,
            T inner,
            ICache<Scope> cache,
            ILogger<CachingScopeStore<T>> logger
        ) {
            _options = options;
            _inner = inner;
            _cache = cache;
            _logger = logger;
        }

        public Task<Guid> CreateAsync(Scope item) => _inner.CreateAsync(item);
        public async Task<Scope> GetAsync(Guid id) {
            Scope item = await _cache.GetAsync(
                id.ToString(),
                _options.Caching.ScopeStoreExpiration,
                () => _inner.GetAsync(id),
                _logger
            );

            return item;
        }

        public IQueryable<Scope> Query() => _inner.Query();
        public async Task RemoveAsync(Guid id) {
            await _inner.RemoveAsync(id);
            await _cache.RemoveAsync(id.ToString());
        }
        public async Task UpdateAsync(Guid id, Scope item) {
            await _inner.UpdateAsync(id, item);
            await _cache.ExpireAsync(id.ToString());
        }
    }
}
