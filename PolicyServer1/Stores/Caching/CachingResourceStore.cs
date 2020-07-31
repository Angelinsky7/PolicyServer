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
    public class CachingResourceStore<T> : IResourceStore where T : IResourceStore {

        private readonly T _inner;
        private readonly PolicyServerOptions _options;
        private readonly ICache<Resource> _cache;
        private readonly ILogger _logger;

        public CachingResourceStore(
            PolicyServerOptions options,
            T inner,
            ICache<Resource> cache,
            ILogger<CachingResourceStore<T>> logger
        ) {
            _options = options;
            _inner = inner;
            _cache = cache;
            _logger = logger;
        }

        public Task<Guid> CreateAsync(Resource item) => _inner.CreateAsync(item);
        public async Task<Resource> GetAsync(Guid id) {
            Resource item = await _cache.GetAsync(
                id.ToString(),
                _options.Caching.ResourceStoreExpiration,
                () => _inner.GetAsync(id),
                _logger
            );

            return item;
        }

        public IQueryable<Resource> Query() => _inner.Query();
        public async Task RemoveAsync(Guid id) {
            await _inner.RemoveAsync(id);
            await _cache.RemoveAsync(id.ToString());
        }
        public async Task UpdateAsync(Guid id, Resource item) {
            await _inner.UpdateAsync(id, item);
            await _cache.ExpireAsync(id.ToString());
        }
    }
}
