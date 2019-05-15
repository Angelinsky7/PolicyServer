using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Mappers;
using PolicyServer1.Models;
using PolicyServer1.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.EntityFramework.Storage.Stores {
    public class RoleStore : IRoleStore {

        private readonly IPolicyDbContext _context;
        private readonly ILogger _logger;

        public RoleStore(
            IPolicyDbContext context,
            ILogger<RoleStore> logger
        ) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<PolicyRole> GetAsync((Int32 policyId, Int32 roleId) key) {
            Entities.Role role = await _context.Roles
                .Include(p => p.IdentityRoles)
                .Include(p => p.Subjects)
                .Include(p => p.Parents)
                    .ThenInclude(p => p.Parent)
                .Include(p => p.Permissions)
                    .ThenInclude(p => p.Permission)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PolicyId == key.policyId &&
                                          p.Id == key.roleId);

            Models.PolicyRole model = role?.ToModel();

            _logger.LogDebug($"{key} found in database: {model != null}");

            return model;
        }

        public async Task<Int32> CreateAsync(PolicyRole newRole) {
            Entities.Role model = newRole.ToEntity();

            _context.Roles.Add(model);
            
            await _context.SaveChangesAsync();

            return model.Id;
        }

        public async Task UpdateAsync((Int32 policyId, Int32 roleId) key, PolicyRole role) {
            if (key.policyId!= role.PolicyId|| key.roleId!= role.Id) { throw new ArgumentException(nameof(role)); }

            Entities.Role model = await _context.Roles
                .SingleOrDefaultAsync(p => p.PolicyId == key.policyId &&
                                           p.Id == key.roleId);
            model = role.ToEntity(model);
            model.Updated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync((Int32 policyId, Int32 roleId) key) {
            Entities.Role model = await _context.Roles
                .SingleOrDefaultAsync(p => p.PolicyId == key.policyId &&
                                           p.Id == key.roleId);

            if (model == null) { throw new ArgumentException(nameof(model)); }

            _context.Roles.Remove(model);

            await _context.SaveChangesAsync();
        }

    }
}
