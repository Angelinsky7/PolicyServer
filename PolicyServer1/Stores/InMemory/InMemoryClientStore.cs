using PolicyServer1.Extensions;
using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores.InMemory {
    public class InMemoryClientStore : IClientStore {

        private readonly List<Client> _clients;

        public InMemoryClientStore(IEnumerable<Client> clients) {
            if (clients.HasDuplicates(p => p.ClientId)) {
                throw new ArgumentException("Clients must not contain duplicate id");
            }
            _clients = new List<Client>(clients);
        }

        public Task<Client> CreateAsync(Client item) {
            _clients.Add(item);
            return Task.FromResult(_clients.Find(p => p.Id == item.Id));
        }

        public Task<Client> GetAsync(Guid id) {
            Client query = _clients.SingleOrDefault(p => p.Id == id);
            return Task.FromResult(query);
        }

        public IQueryable<Client> Get() {
            return _clients.AsQueryable();
        }

        public Task<Client> GetFromClientIdAsync(String clientId) {
            Client query = _clients.SingleOrDefault(p => p.ClientId == clientId);
            return Task.FromResult(query);
        }

        public Task<Client> RemoveAsync(Guid id) {
            Client client = _clients.SingleOrDefault(p => p.Id == id);
            if (client != null) {
                _clients.Remove(client);
            }
            return Task.FromResult(client);
        }

        public Task<Client> RemoveClientIdAsync(String cliendId) {
            Client client = _clients.SingleOrDefault(p => p.ClientId == cliendId);
            if (client != null) {
                _clients.Remove(client);
            }
            return Task.FromResult(client);
        }

        public Task<Client> UpdateAsync(Guid id, Client item) {
            if (id != item.Id) { throw new ArgumentException(nameof(id)); }
            Int32 index = _clients.FindIndex(p => p.Id == item.Id);
            if (index == -1) { throw new ArgumentException(nameof(item)); }
            _clients[index] = item;
            return Task.FromResult(item);
        }

    }
}
