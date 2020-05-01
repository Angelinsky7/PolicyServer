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
    public class CachingClientStore<T> : IClientStore where T : IClientStore {

        private readonly T _inner;
        private readonly PolicyServerOptions _options;
        private readonly ICache<Client> _cache;
        private readonly ILogger _logger;

        public CachingClientStore(
            PolicyServerOptions options,
            T inner,
            ICache<Client> cache,
            ILogger<CachingClientStore<T>> logger
        ) {
            _options = options;
            _inner = inner;
            _cache = cache;
            _logger = logger;
        }

        public Task<Guid> CreateAsync(Client item) {
            return _inner.CreateAsync(item);
        }

        public Task<Client> GetAsync(Guid id) {
            return _inner.GetAsync(id);
        }

        public IQueryable<Client> Query() {
            return _inner.Query();
        }

        public Task<Client> GetFromClientIdAsync(String clientId) {
            return _inner.GetFromClientIdAsync(clientId);
        }

        public Task RemoveAsync(Guid id) {
            return _inner.RemoveAsync(id);
        }

        public Task RemoveClientIdAsync(String cliendId) {
            return _inner.RemoveClientIdAsync(cliendId);
        }

        public Task UpdateAsync(Guid id, Client item) {
            return _inner.UpdateAsync(id, item);
        }

        //public Task<Int32> CreateAsync(Client newClient) {
        //    throw new NotImplementedException();
        //}

        //public Task<Client> GetFromClientIdAsync(String clientId) {
        //    throw new NotImplementedException();
        //}

        //public async Task<Client> GetAsync(Int32 clientId) {
        //    Client client = await _cache.GetAsync(
        //        clientId.ToString(),
        //        _options.Caching.ClientStoreExpiration,
        //        () => _inner.GetAsync(clientId),
        //        _logger
        //    );

        //    return client;
        //}

        //public Task RemoveAsync(Int32 cliendId) {
        //    throw new NotImplementedException();
        //}

        //public Task RemoveClientIdAsync(String cliendId) {
        //    throw new NotImplementedException();
        //}

        //public Task UpdateAsync(Int32 clientId, Client client) {
        //    throw new NotImplementedException();
        //}

        //public Task<Models.Client> ReloadAsync(Client client) {
        //    throw new NotImplementedException();
        //}

    }
}
