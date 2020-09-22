using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Mappers;
using PolicyServer1.Models;
using PolicyServer1.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.EntityFramework.Storage.Stores {
    public class PolicyStore : IPolicyStore {

        private readonly IPolicyDbContext _context;
        private readonly ILogger _logger;

        public PolicyStore(
            IPolicyDbContext context,
            ILogger<PolicyStore> logger
        ) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(Policy item) {
            Entities.Policy entity = item.ToEntity();

            //_context.MarkEntitesAsDetached<Entities.Scope>();

            _context.Policies.Add(entity);

            //_context.MarkEntitesAsUnchanged<Entities.Scope>();

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception adding {entity} to database: {ex.Message}");
            }

            return entity.Id;
        }

        public async Task<Policy> GetAsync(Guid id) {
            Entities.Policy entity = await GetPolicies()
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
                return null;
            }

            return entity.ToModel();
        }

        public IQueryable<Policy> Query() {
            //var result = GetPolicies().Select(PolicyMappers.Policy.Projection);

            //TODO(demarco): should not be a in memroy thing....
            List<Policy> result = new List<Policy>();
            foreach (var item in GetPolicies().AsNoTracking()) {
                result.Add(item.ToModel());
            }
            return result.AsQueryable();

        }

        public async Task RemoveAsync(Guid id) {
            Entities.Policy entity = await GetPolicies().SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            _context.Policies.Remove(entity);

            try {
                await _context.SaveChangesAsync();
                await RemoveAllOrpanedTimeRangeAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception removing {entity} to database: {ex.Message}");
            }
        }

        public async Task UpdateAsync(Guid id, Policy item) {
            Entities.Policy entity = await GetPolicies()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            _context.MarkEntitesAsUnchanged<Entities.Role>();

            item.UpdateEntity(entity);
            entity.Updated = DateTime.UtcNow;

            await _context.MarkEntitesAsUnchangedWithHackAsync<Entities.Role>();

            try {
                await _context.SaveChangesAsync();
                await RemoveAllOrpanedTimeRangeAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception updating {item} to database: {ex.Message}");
            }
        }

        private async Task RemoveAllOrpanedTimeRangeAsync() {
            try {
                foreach (var item in _context.Set<Entities.TimePolicyRange>().ToList()) {
                    try {
                        _context.Set<Entities.TimePolicyRange>().Remove(item);
                        await _context.SaveChangesAsync();
                    } catch { }
                }
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception when removing all time range orphan from database: {ex.Message}");
            }
        }

        private IQueryable<Entities.Policy> GetPolicies() => _context.Policies
            .Include(p => (p as Entities.RolePolicy).Roles)
                .ThenInclude(p => p.Role)
            .Include(p => (p as Entities.TimePolicy).DayOfMonth)
            .Include(p => (p as Entities.TimePolicy).Month)
            .Include(p => (p as Entities.TimePolicy).Year)
            .Include(p => (p as Entities.TimePolicy).Hour)
            .Include(p => (p as Entities.TimePolicy).Minute)
            .Include(p => (p as Entities.ClientPolicy).Clients)
            .Include(p => (p as Entities.UserPolicy).Users)
            .Include(p => (p as Entities.GroupPolicy).Groups)
            .Include(p => (p as Entities.AggregatedPolicy).Policies)
                .ThenInclude(p => p.Policy);

        //public async Task<Models.Policy> GetAsync(Int32 policyId) {
        //    Entities.Policy policy = await _context.Policies
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(p => p.Id == policyId);

        //    Models.Policy model = policy?.ToModel();

        //    _logger.LogDebug($"{policyId} found in database: {model != null}");

        //    return model;
        //}

        //public async Task<Int32> CreateAsync(Models.Policy newPolicy) {
        //    Entities.Policy model = newPolicy.ToEntity();

        //    _context.Policies.Add(model);

        //    await _context.SaveChangesAsync();

        //    return model.Id;
        //}

        //public async Task UpdateAsync(Int32 policyId, Models.Policy policy) {
        //    if (policyId != policy.Id) { throw new ArgumentException(nameof(policyId)); }

        //    Entities.Policy model = await _context.Policies.SingleOrDefaultAsync(p => p.Id == policyId);
        //    model = policy.ToEntity(model);
        //    model.Updated = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();
        //}

        //public async Task RemoveAsync(Int32 policyId) {
        //    Entities.Policy model = await _context.Policies.SingleOrDefaultAsync(p => p.Id == policyId);
        //    if (model == null) { throw new ArgumentException(nameof(model)); }

        //    _context.Policies.Remove(model);

        //    await _context.SaveChangesAsync();
        //}

    }
}
