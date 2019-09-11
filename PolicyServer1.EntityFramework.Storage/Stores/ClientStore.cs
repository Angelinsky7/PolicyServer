using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Mappers;
using PolicyServer1.Models;
using PolicyServer1.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.EntityFramework.Storage.Stores {
    public class ClientStore : IClientStore {

        private readonly IPolicyDbContext _context;
        private readonly IPolicyStore _policyStore;
        private readonly ILogger _logger;

        public ClientStore(
            IPolicyDbContext context,
            IPolicyStore policyStore,
            ILogger<ClientStore> logger
        ) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _policyStore = policyStore;
            _logger = logger;
        }

        public Task<Client> CreateAsync(Client item) {
            throw new NotImplementedException();
        }

        public Task<Client> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public IQueryable<Client> Get() {
            throw new NotImplementedException();
        }

        public Task<Client> GetFromClientIdAsync(String clientId) {
            throw new NotImplementedException();
        }

        public Task<Client> RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Client> RemoveClientIdAsync(String cliendId) {
            throw new NotImplementedException();
        }

        public Task<Client> UpdateAsync(Guid id, Client item) {
            throw new NotImplementedException();
        }

        //public async Task<Models.Client> GetAsync(Int32 clientId) {
        //    return await GetFromExpessionAsync(clientId, p => p.Id == clientId);
        //}

        //public async Task<Models.Client> GetFromClientIdAsync(String clientId) {
        //    return await GetFromExpessionAsync(clientId, p => p.ClientId == clientId);
        //}

        //private async Task<Models.Client> GetFromExpessionAsync<T>(T key, Expression<Func<Entities.Client, Boolean>> predicate) {
        //    Entities.Client client = await _context.Clients
        //        .Include(p => p.Policy)
        //            .ThenInclude(p => p.Roles)
        //        .Include(p => p.Policy)
        //            .ThenInclude(p => p.Permissions)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(predicate);

        //    Models.Client model = client?.ToModel();

        //    _logger.LogDebug($"{key} found in database: {model != null}");

        //    return model;
        //}

        //public Task<Int32> CreateAsync(Models.Client newClient) {
        //    Int32 result = -1;

        //    //TODO(demarco): Rebuild this after the changes !
        //    //using (IDbContextTransaction transaction = await _context.BeginTransactionAsync()) {
        //    //    Entities.Client model = newClient.ToEntity();

        //    //    Int32 policyId = await _policyStore.CreateAsync(new Models.Policy {
        //    //        LastPolicyChangeDate = DateTime.Now
        //    //    });

        //    //    model.PolicyId = policyId;

        //    //    _context.Clients.Add(model);

        //    //    await _context.SaveChangesAsync();

        //    //    transaction.Commit();

        //    //    result = model.Id;
        //    //}

        //    return Task.FromResult(result);
        //}

        //public async Task UpdateAsync(Int32 clientId, Models.Client client) {
        //    //TODO(demarco): Rebuild this after the changes !
        //    //if (clientId != client.Id) { throw new ArgumentException(nameof(clientId)); }

        //    Entities.Client model = await _context.Clients.SingleOrDefaultAsync(p => p.Id == clientId);
        //    model = client.ToEntity(model);
        //    model.Updated = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();
        //}

        //public async Task RemoveAsync(Int32 cliendId) {
        //    await RemoveExpressionAsync(p => p.Id == cliendId);
        //}

        //public async Task RemoveClientIdAsync(String cliendId) {
        //    await RemoveExpressionAsync(p => p.ClientId == cliendId);
        //}

        //private async Task RemoveExpressionAsync(Expression<Func<Entities.Client, Boolean>> predicate) {
        //    using (IDbContextTransaction transaction = await _context.BeginTransactionAsync()) {
        //        Entities.Client model = await _context.Clients.SingleOrDefaultAsync(predicate);
        //        if (model == null) { throw new ArgumentException(nameof(model)); }

        //        _context.Clients.Remove(model);

        //        await _context.SaveChangesAsync();

        //        //TODO(demarco): Rebuild this after the changes !
        //        //if (model.PolicyId != null) { await _policyStore.RemoveAsync(model.PolicyId.Value); }

        //        transaction.Commit();
        //    }
        //}
    }
}
