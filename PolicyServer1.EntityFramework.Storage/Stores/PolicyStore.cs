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

        public Task<Guid> CreateAsync(Policy item) {
            throw new NotImplementedException();
        }

        public Task<Policy> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public IQueryable<Policy> Query() {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Policy item) {
            throw new NotImplementedException();
        }

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
