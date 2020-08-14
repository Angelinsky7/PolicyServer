using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Mappers;
using PolicyServer1.Models;
using PolicyServer1.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            Entities.Permission entity = item.ToEntity();

            _context.MarkEntitesAsDetached<Entities.Policy>();

            _context.Permissions.Add(entity);

            _context.MarkEntitesAsUnchanged<Entities.Policy>();

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception adding {entity} to database: {ex.Message}");
            }

            return entity.Id;
        }
        public async Task<Permission> GetAsync(Guid id) {
            Entities.Permission entity = await _context.Permissions
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
                return null;
            }

            return entity.ToModel();
        }
        public IQueryable<Permission> Query() => _context.Permissions.AsNoTracking().ToModel();
        public async Task RemoveAsync(Guid id) {
            Entities.Permission entity = await _context.Permissions
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
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
            Entities.Permission entity = await _context.Permissions
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            _context.MarkEntitesAsUnchanged<Entities.Policy>();

            item.UpdateEntity(entity);

            await _context.MarkEntitesAsUnchangedWithHackAsync<Entities.Policy>();

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception updating {item} to database: {ex.Message}");
            }
        }

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
