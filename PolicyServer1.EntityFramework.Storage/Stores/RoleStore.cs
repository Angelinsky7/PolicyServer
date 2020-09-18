using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Mappers;
using PolicyServer1.Models;
using PolicyServer1.Stores;

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

        public async Task<Guid> CreateAsync(Role item) {
            Entities.Role entity = item.ToEntity();

            _context.Roles.Add(entity);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception adding {entity} to database: {ex.Message}");
            }

            return entity.Id;
        }

        public Task<Role> GetAsync(Guid id) {
            //Entities.Role entity = (await _context.Roles
            //    .Include(p => p.Parents)
            //        .ThenInclude(p => p.Role)
            //    //.AsNoTracking()
            //    .SingleOrDefaultAsync(p => p.Id == id));
            Entities.Role entity = GetRoles().SingleOrDefault(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
                return null;
            }

            return Task.FromResult(entity.ToModel());
        }

        //public IQueryable<Role> Query() => _context.Roles
        //    .Include(p => p.Parents)
        //        .ThenInclude(p => p.Role)
        //    .AsNoTracking()
        //    .Select(RoleMappers.Role.Projection);

        public IQueryable<Role> Query() => GetRoles().Select(RoleMappers.Role.Projection);

        public async Task RemoveAsync(Guid id) {
            Entities.Role entity = await _context.Roles
                .Include(p => p.Parents)
                    .ThenInclude(p => p.Role)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            _context.Roles.Remove(entity);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception removing {entity} to database: {ex.Message}");
            }
        }

        public async Task UpdateAsync(Guid id, Role item) {
            Entities.Role entity = await _context.Roles
                .Include(p => p.Parents)
                    .ThenInclude(p => p.Role)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) {
                _logger.LogInformation($"entity with id {id} was not found");
                //throw new EntityNotFoundException(nameof(Trail), id);
            }

            item.UpdateEntity(entity);
            entity.Updated = DateTime.UtcNow;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogInformation($"exception updating {item} to database: {ex.Message}");
            }
        }

        private IQueryable<Entities.Role> GetRoles() => _context.Roles
            .Include(p => p.Parents)
                .ThenInclude(p => p.Parent)
            .ToList()
            .AsQueryable();

        //public async Task<PolicyRole> GetAsync((Int32 policyId, Int32 roleId) key) {
        //    Entities.Role role = await _context.Roles
        //        .Include(p => p.IdentityRoles)
        //        .Include(p => p.Subjects)
        //        .Include(p => p.Parents)
        //            .ThenInclude(p => p.Parent)
        //        .Include(p => p.Permissions)
        //            .ThenInclude(p => p.Permission)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(p => p.PolicyId == key.policyId &&
        //                                  p.Id == key.roleId);

        //    Models.PolicyRole model = role?.ToModel();

        //    _logger.LogDebug($"{key} found in database: {model != null}");

        //    return model;
        //}

        //public async Task<Int32> CreateAsync(PolicyRole newRole) {
        //    Entities.Role model = newRole.ToEntity();

        //    _context.Roles.Add(model);

        //    await _context.SaveChangesAsync();

        //    return model.Id;
        //}

        //public async Task UpdateAsync((Int32 policyId, Int32 roleId) key, PolicyRole role) {
        //    if (key.policyId!= role.PolicyId|| key.roleId!= role.Id) { throw new ArgumentException(nameof(role)); }

        //    Entities.Role model = await _context.Roles
        //        .SingleOrDefaultAsync(p => p.PolicyId == key.policyId &&
        //                                   p.Id == key.roleId);
        //    model = role.ToEntity(model);
        //    model.Updated = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();
        //}

        //public async Task RemoveAsync((Int32 policyId, Int32 roleId) key) {
        //    Entities.Role model = await _context.Roles
        //        .SingleOrDefaultAsync(p => p.PolicyId == key.policyId &&
        //                                   p.Id == key.roleId);

        //    if (model == null) { throw new ArgumentException(nameof(model)); }

        //    _context.Roles.Remove(model);

        //    await _context.SaveChangesAsync();
        //}

    }
}
