using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Mappers;
using PolicyServer1.Models;
using PolicyServer1.Stores;

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

        public async Task<Guid> CreateAsync(Client item) {
            Entities.Client entity = item.ToEntity();

            //TODO(demarco): Make this behavbe like the others store, look at ResourceStore to have an example
            _context.MarkEntitesAsDetached<Entities.Role>();
            _context.MarkEntitesAsDetached<Entities.MmRoleRole>();
            _context.MarkEntitesAsDetached<Entities.MmRolePolicyRole>();
            _context.MarkEntitesAsDetached<Entities.Scope>();
            _context.MarkEntitesAsDetached<Entities.Resource>();
            _context.MarkEntitesAsDetached<Entities.MmResourceScope>();

            //NOTE(demarco): I'm not sure about that part
            _context.MarkEntitesAsDetached<Entities.Permission>();
            _context.MarkEntitesAsDetached<Entities.MmPermissionPolicy>();
            //NOTE(demarco): I'm not sure about that part

            _context.MarkEntitesAsDetached<Entities.Policy>();
            _context.MarkEntitesAsDetached<Entities.MmAggregatedPolicyPolicy>();

            _context.Clients.Add(entity);

            await _context.CheckExistingAndRemoveAsync<Entities.Role>((existing, added) => existing.Id == added.Id);
            await _context.CheckExistingAndRemoveAsync<Entities.MmRoleRole>((existing, added) => existing.ParentId == added.ParentId && existing.RoleId == added.RoleId);
            await _context.CheckExistingAndRemoveAsync<Entities.MmRolePolicyRole>((existing, added) => existing.RoleId == added.RoleId && existing.RolePolicyId == added.RolePolicyId);
            await _context.CheckExistingAndRemoveAsync<Entities.Scope>((existing, added) => existing.Id == added.Id);
            await _context.CheckExistingAndRemoveAsync<Entities.Resource>((existing, added) => existing.Id == added.Id);
            await _context.CheckExistingAndRemoveAsync<Entities.MmResourceScope>((existing, added) => existing.ResourceId == added.ResourceId && existing.ScopeId == added.ScopeId);

            //NOTE(demarco): I'm not sure about that part
            await _context.CheckExistingAndRemoveAsync<Entities.Permission>((existing, added) => existing.Id == added.Id);
            await _context.CheckExistingAndRemoveAsync<Entities.MmPermissionPolicy>((existing, added) => existing.PermissionId == added.PermissionId && existing.PolicyId == added.PolicyId);
            //NOTE(demarco): I'm not sure about that part

            await _context.CheckExistingAndRemoveAsync<Entities.Policy>((existing, added) => existing.Id == added.Id);
            await _context.CheckExistingAndRemoveAsync<Entities.MmAggregatedPolicyPolicy>((existing, added) => existing.AggregatedPolicyId == added.AggregatedPolicyId && existing.PolicyId == added.PolicyId);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception adding {entity} to database: {ex.Message}");
            }

            return entity.Id;
        }

        public async Task<Client> GetAsync(Guid id) {
            Entities.Client entity = await _context.Clients.SingleOrDefaultAsync(p => p.Id == id);
            await entity.LoadDatasAsync(_context);
            //Entities.Client entity = await _context.Clients.IncludeQuery().SingleOrDefaultAsync(p => p.Id == id);
            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
                return null;
            }

            return entity.ToModel();
        }

        public IQueryable<Client> Query() => _context.Clients.IncludeQuery().ToModel();

        public async Task<Client> GetFromClientIdAsync(String clientId) {
            Entities.Client entity = await _context.Clients.SingleOrDefaultAsync(p => p.ClientId == clientId);
            await entity.LoadDatasAsync(_context);
            //Entities.Client entity = await _context.Clients.IncludeQuery().SingleOrDefaultAsync(p => p.ClientId == clientId);
            if (entity == null) {
                _logger.LogInformation($"entity with id {clientId} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
                return null;
            }

            return entity.ToModel();
        }

        public async Task RemoveAsync(Guid id) {
            Entities.Client entity = await _context.Clients.SingleOrDefaultAsync(p => p.Id == id);
            await entity.LoadDatasAsync(_context);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }
                        
            _context.Clients.Remove(entity);
            //_context.Set<Entities.Secret>().RemoveRange(entity.Secrets.Select(p => p.Secret));

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception removing {entity} to database: {ex.Message}");
            }
        }

        public async Task RemoveClientIdAsync(String cliendId) {
            Entities.Client entity = await _context.Clients.SingleOrDefaultAsync(p => p.ClientId == cliendId);
            await entity.LoadDatasAsync(_context);

            if (entity == null) {
                _logger.LogInformation($"entity with id {cliendId} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            _context.Clients.Remove(entity);
            //_context.Set<Entities.Secret>().RemoveRange(entity.Secrets.Select(p => p.Secret));

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception removing {entity} to database: {ex.Message}");
            }
        }

        public async Task UpdateAsync(Guid id, Client item) {
            Entities.Client entity = await _context.Clients.SingleOrDefaultAsync(p => p.Id == id);
            await entity.LoadDatasAsync(_context);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            //TODO(demarco): Make this behavbe like the others store, look at ResourceStore to have an example

            item.UpdateEntity(entity);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception updating {item} to database: {ex.Message}");
            }
        }

    }
}
