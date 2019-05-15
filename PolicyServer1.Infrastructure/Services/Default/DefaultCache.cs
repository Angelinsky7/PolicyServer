using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace PolicyServer1.Services.Default {
    public class DefaultCache<T> : ICache<T> where T : class {

        private const String KeySeparator = ":";
        private readonly IMemoryCache _cache;

        public DefaultCache(IMemoryCache cache) {
            _cache = cache;
        }

        public Task<T> GetAsync(String key) {
            key = GetKey(key);
            T item = _cache.Get<T>(key);
            return Task.FromResult(item);
        }

        public Task SetAsync(String key, T item, TimeSpan expiration) {
            key = GetKey(key);
            _cache.Set(key, item, expiration);
            return Task.CompletedTask;
        }

        private String GetKey(String key) {
            return typeof(T).FullName + KeySeparator + key;
        }

    }
}
