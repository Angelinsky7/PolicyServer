using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Mappers;
using PolicyServer1.Models;
using PolicyServer1.Stores;

namespace PolicyServer1.EntityFramework.Storage.Stores {
    public class PermissionStore : IPermissionStore {

        private readonly IPolicyDbContext _context;
        private readonly IPolicyStore _policyStore;
        private readonly ILogger _logger;

        public PermissionStore(
            IPolicyDbContext context,
            IPolicyStore policyStore,
            ILogger<PermissionStore> logger
        ) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _policyStore = policyStore;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(Permission item) {
            if (item.GetType() == typeof(Permission)) { throw new ArgumentException("Cannot insert an abstract permission."); }

            Entities.Permission entity = item.ToEntity();

            _context.MarkEntitesAsUnchanged<Entities.Policy>();
            _context.MarkEntitesAsUnchanged<Entities.Resource>();
            _context.MarkEntitesAsUnchanged<Entities.Scope>();
            _context.MarkEntitesAsUnchanged<Entities.MmResourceScope>();

            _context.Permissions.Add(entity);

            _context.MarkEntitesAsUnchanged<Entities.Policy>();
            _context.MarkEntitesAsUnchanged<Entities.Resource>();
            _context.MarkEntitesAsUnchanged<Entities.Scope>();
            _context.MarkEntitesAsUnchanged<Entities.MmResourceScope>();

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception adding {entity} to database: {ex.Message}");
            }

            return entity.Id;
        }
        public async Task<Permission> GetAsync(Guid id) {
            Entities.Permission entity = await GetPermissions()
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
                return null;
            }

            return entity.ToModel();
        }

        public IQueryable<Permission> Query() {

            //TODO(demarco): should not be a in memroy thing....
            List<Permission> result = new List<Permission>();
            foreach (var item in GetPermissions()
                .AsNoTracking()) {
                result.Add(item.ToModel());
            }
            return result.AsQueryable();

            //IQueryable<ScopePermission> scopePermissions = _context.Permissions.OfType<Entities.ScopePermission>().AsNoTracking().ToModel<Models.ScopePermission, Entities.ScopePermission>();
            //IQueryable<ResourcePermission> resourcePermissions = _context.Permissions.OfType<Entities.ResourcePermission>().AsNoTracking().ToModel<Models.ResourcePermission, Entities.ResourcePermission>();

            //IQueryable<Permission> result = scopePermissions.Cast<Permission>().Concat(resourcePermissions);
            //return result;
        }

        //public IQueryable<Permission> Query() => _context.Permissions
        //    .Include(p => p.Policies)
        //        .ThenInclude(p => p.Policy)
        //    .Include(p => (p as Entities.ScopePermission).Resource)
        //    .Include(p => (p as Entities.ScopePermission).Scopes)
        //            .ThenInclude(p => p.Scope)
        //    .Include(p => (p as Entities.ResourcePermission).Resource)
        //    .AsNoTracking()
        //    .UseAsDataSource(PermissionMappers.Mapper)
        //    .For<Permission>();

        public async Task RemoveAsync(Guid id) {
            Entities.Permission entity = await GetPermissions()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            _context.Permissions.Remove(entity);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception removing {entity} to database: {ex.Message}");
            }
        }
        public async Task UpdateAsync(Guid id, Permission item) {
            if (item.GetType() == typeof(Permission)) { throw new ArgumentException("Cannot update an abstract permission."); }

            Entities.Permission entity = await GetPermissions()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            _context.MarkEntitesAsUnchanged<Entities.Policy>();
            _context.MarkEntitesAsUnchanged<Entities.Resource>();
            _context.MarkEntitesAsUnchanged<Entities.Scope>();
            _context.MarkEntitesAsUnchanged<Entities.MmResourceScope>();

            item.UpdateEntity(entity);
            entity.Updated = DateTime.UtcNow;

            await _context.MarkEntitesAsUnchangedWithHackAsync<Entities.Policy>();
            await _context.MarkEntitesAsUnchangedWithHackAsync<Entities.Resource>();
            await _context.MarkEntitesAsUnchangedWithHackAsync<Entities.Scope>();
            await _context.MarkEntitesAsUnchangedWithHackAsync<Entities.MmResourceScope>();

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception updating {item} to database: {ex.Message}");
            }
        }

        private IQueryable<Entities.Permission> GetPermissions() => _context.Permissions
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                .Include(p => (p as Entities.ScopePermission).Resource)
                .Include(p => (p as Entities.ScopePermission).Scopes)
                     .ThenInclude(p => p.Scope)
                .Include(p => (p as Entities.ResourcePermission).Resource);

        //public Task<Permission> GetByNameAsync(String name) {
        //    Entities.Permission entity = await _context.Permissions
        //        .Include(p => p.Policies)
        //            .ThenInclude(p => p.Policy)
        //        .AsNoTracking()
        //        .SingleOrDefaultAsync(p => p.Name == name);

        //    if (entity == null) {
        //        _logger.LogInformation($"entity with id {id} was not found");
        //        //throw new EntityNotFoundException(nameof(Trail), id);
        //        return null;
        //    }

        //    return entity.ToModel();
        //}

    }
}
