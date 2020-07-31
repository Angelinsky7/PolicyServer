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
    public class ScopeStore : IScopeStore {

        private readonly IPolicyDbContext _context;
        private readonly IPolicyStore _policyStore;
        private readonly ILogger _logger;

        public ScopeStore(
            IPolicyDbContext context,
            IPolicyStore policyStore,
            ILogger<ScopeStore> logger
        ) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _policyStore = policyStore;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(Scope item) {
            Entities.Scope entity = item.ToEntity();

            _context.Scopes.Add(entity);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception adding {entity} to database: {ex.Message}");
            }

            return entity.Id;
        }
        public async Task<Scope> GetAsync(Guid id) {
            Entities.Scope entity = (await _context.Scopes.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id));

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
                return null;
            }

            return entity.ToModel();
        }
        public IQueryable<Scope> Query() => _context.Scopes.AsNoTracking().ToModel();
        public async Task RemoveAsync(Guid id) {
            Entities.Scope entity = await _context.Scopes.SingleOrDefaultAsync(p => p.Id == id);
           
            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }


            _context.Scopes.Remove(entity);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception removing {entity} to database: {ex.Message}");
            }
        }
        public async Task UpdateAsync(Guid id, Scope item) {
            Entities.Scope entity = await _context.Scopes.SingleOrDefaultAsync(p => p.Id == id);
            
            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            item.UpdateEntity(entity);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception updating {item} to database: {ex.Message}");
            }
        }
    }
}
