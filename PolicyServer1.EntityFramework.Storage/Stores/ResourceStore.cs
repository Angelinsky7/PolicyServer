using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Mappers;
using PolicyServer1.Models;
using PolicyServer1.Stores;

namespace PolicyServer1.EntityFramework.Storage.Stores {
    public class ResourceStore : IResourceStore {

        private readonly IPolicyDbContext _context;
        private readonly IPolicyStore _policyStore;
        private readonly ILogger _logger;

        public ResourceStore(
            IPolicyDbContext context,
            IPolicyStore policyStore,
            ILogger<ResourceStore> logger
        ) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _policyStore = policyStore;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(Resource item) {
            Entities.Resource entity = item.ToEntity();

            _context.MarkEntitesAsDetached<Entities.Scope>();
            
            _context.Resources.Add(entity);

            _context.MarkEntitesAsUnchanged<Entities.Scope>();

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception adding {entity} to database: {ex.Message}");
            }

            return entity.Id;
        }
        public async Task<Resource> GetAsync(Guid id) {
            Entities.Resource entity = await _context.Resources
                .Include(p => p.Uris)
                .Include(p => p.Scopes)
                    .ThenInclude(p => p.Scope)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
                return null;
            }

            return entity.ToModel();
        }
        public IQueryable<Resource> Query() => _context.Resources.AsNoTracking().ToModel();
        public async Task RemoveAsync(Guid id) {
            Entities.Resource entity = await _context.Resources
                .Include(p => p.Uris)
                .Include(p => p.Scopes)
                    .ThenInclude(p => p.Scope)
                .SingleOrDefaultAsync(p => p.Id == id);
           
            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            _context.Resources.Remove(entity);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception removing {entity} to database: {ex.Message}");
            }
        }
        public async Task UpdateAsync(Guid id, Resource item) {
            Entities.Resource entity = await _context.Resources
                .Include(p => p.Uris)
                .Include(p => p.Scopes)
                    .ThenInclude(p => p.Scope)
                .SingleOrDefaultAsync(p => p.Id == id);
            
            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            _context.MarkEntitesAsUnchanged<Entities.Scope>();

            item.UpdateEntity(entity);
                        
            await _context.MarkEntitesAsUnchangedWithHackAsync<Entities.Scope>();

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception updating {item} to database: {ex.Message}");
            }
        }
    }
}

