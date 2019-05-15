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

        public Task<Client> GetAsync(Int32 clientId) {
            Client query = _clients.SingleOrDefault(p => p.Id == clientId);
            return Task.FromResult(query);
        }

        public Task<Client> GetFromClientIdAsync(String clientId) {
            Client query = _clients.SingleOrDefault(p => p.ClientId == clientId);
            return Task.FromResult(query);
        }

        public Task<Int32> CreateAsync(Client newClient) {
            _clients.Add(newClient);
            return Task.FromResult(_clients.FindIndex(p => p.Id == newClient.Id));
        }

        public Task UpdateAsync(Int32 clientId, Client client) {
            if (clientId != client.Id) { throw new ArgumentException(nameof(clientId)); }
            Int32 index = _clients.FindIndex(p => p.Id == clientId);
            if (index == -1) { throw new ArgumentException(nameof(clientId)); }
            _clients[index] = client;
            return Task.FromResult(0);
        }

        public Task RemoveAsync(Int32 cliendId) {
            Client client = _clients.SingleOrDefault(p => p.Id == cliendId);
            if (client != null) {
                _clients.Remove(client);
            }
            return Task.FromResult(0);
        }

        public Task RemoveClientIdAsync(String cliendId) {
            Client client = _clients.SingleOrDefault(p => p.ClientId == cliendId);
            if (client != null) {
                _clients.Remove(client);
            }
            return Task.FromResult(0);
        }

        public Task<Models.Client> ReloadAsync(Client client) {
            return Task.FromResult(client);
        }

    }
}
