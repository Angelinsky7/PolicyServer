using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PolicyServer1.Services;

namespace PolicyServer1.Extensions {
    public static class ICacheExtensions {

        public static async Task<T> GetAsync<T>(
            this ICache<T> cache,
            String key,
            TimeSpan duration,
            Func<Task<T>> get,
            ILogger logger
        ) where T : class {
            if (cache == null) { throw new ArgumentNullException(nameof(cache)); }
            if (get == null) { throw new ArgumentNullException(nameof(get)); }
            if (key == null) { return null; }

            T item = await cache.GetAsync(key);
            if(item == null) {
                logger.LogTrace("Cache miss for {cacheKey}", key);
                item = await get();
                if(item != null) {
                    logger.LogTrace("Setting item in cache for {cacheKey}", key);
                    await cache.SetAsync(key, item, duration);
                }
            } else {
                logger.LogTrace("Cache hit for {cacheKey}", key);
            }

            return item;
        }

    }
}
