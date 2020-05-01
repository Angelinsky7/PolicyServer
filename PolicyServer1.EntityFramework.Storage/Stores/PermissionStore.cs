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
        private readonly ILogger _logger;

        public PermissionStore(
            IPolicyDbContext context,
            ILogger<PolicyStore> logger
        ) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public Task<Guid> CreateAsync(Permission item) {
            throw new NotImplementedException();
        }

        public Task<Permission> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public IQueryable<Permission> Query() {
            throw new NotImplementedException();
        }

        public Task<Permission> GetByNameAsync(String name) {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Permission item) {
            throw new NotImplementedException();
        }

        //public async Task<PolicyPermission> GetAsync((Int32 policyId, Int32 permissionId) key) {
        //    return await GetPermissionAsync(key, p => p.PolicyId == key.policyId && p.Id == key.permissionId);
        //}

        //public async Task<PolicyPermission> GetByNameAsync(Int32 policyId, String permissionName) {
        //    return await GetPermissionAsync(policyId, p => p.PolicyId == policyId && p.Name == permissionName);
        //}

        //private async Task<PolicyPermission> GetPermissionAsync(Object key, Expression<Func<Entities.Permission, Boolean>> predicate) {
        //    Entities.Permission permission = await _context.Permissions
        //        .Include(p => p.Policy)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(predicate);

        //    Models.PolicyPermission model = permission?.ToModel();

        //    _logger.LogDebug($"{key} found in database: {model != null}");

        //    return model;
        //}

        //public async Task<Int32> CreateAsync(PolicyPermission newPermission) {
        //    Entities.Permission model = newPermission.ToEntity();

        //    _context.Permissions.Add(model);

        //    await _context.SaveChangesAsync();

        //    return model.Id;
        //}

        //public async Task UpdateAsync((Int32 policyId, Int32 permissionId) key, PolicyPermission permission) {
        //    if (key.policyId != permission.PolicyId || key.permissionId != permission.Id) { throw new ArgumentException(nameof(permission)); }

        //    Entities.Permission model = await _context.Permissions
        //        .SingleOrDefaultAsync(p => p.PolicyId == key.policyId &&
        //                                   p.Id == key.permissionId);
        //    model = permission.ToEntity(model);
        //    model.Updated = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();
        //}

        //public async Task RemoveAsync((Int32 policyId, Int32 permissionId) key) {
        //    Entities.Permission model = await _context.Permissions
        //        .SingleOrDefaultAsync(p => p.PolicyId == key.policyId &&
        //                                   p.Id == key.policyId);

        //    if (model == null) { throw new ArgumentException(nameof(model)); }

        //    _context.Permissions.Remove(model);

        //    await _context.SaveChangesAsync();
        //}

    }
}
