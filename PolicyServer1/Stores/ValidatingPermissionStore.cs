using Microsoft.Extensions.Logging;
using PolicyServer1.Models;
using PolicyServer1.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public class ValidatingPermissionStore<T> : IPermissionStore where T : IPermissionStore {

        private readonly T _inner;
        private readonly IPermissionConfigurationValidator _validator;
        private readonly ILogger<ValidatingPermissionStore<T>> _logger;
        private readonly String _validatorType;

        public ValidatingPermissionStore(
            T inner,
            IPermissionConfigurationValidator validator,
            ILogger<ValidatingPermissionStore<T>> logger
        ) {
            _inner = inner;
            _validator = validator;
            _logger = logger;
            _validatorType = _validator.GetType().FullName;
        }

        public Task<Permission> CreateAsync(Permission item) {
            throw new NotImplementedException();
        }

        public Task<Permission> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public IQueryable<Permission> Get() {
            throw new NotImplementedException();
        }

        public Task<Permission> GetByNameAsync(String name) {
            throw new NotImplementedException();
        }

        public Task<Permission> RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Permission> UpdateAsync(Guid id, Permission item) {
            throw new NotImplementedException();
        }

        //public Task<Int32> CreateAsync(PolicyPermission newPermission) {
        //    return _inner.CreateAsync(newPermission);
        //}

        //public Task<PolicyPermission> GetAsync((Int32 policyId, Int32 permissionId) key) {
        //    return _inner.GetAsync(key);
        //}

        //public Task<PolicyPermission> GetByNameAsync(Int32 policyId, String permissionName) {
        //    return _inner.GetByNameAsync(policyId, permissionName);
        //}

        //public Task RemoveAsync((Int32 policyId, Int32 permissionId) key) {
        //    return _inner.RemoveAsync(key);
        //}

        //public Task UpdateAsync((Int32 policyId, Int32 permissionId) key, PolicyPermission permission) {
        //    return _inner.UpdateAsync(key, permission);
        //}
    }
}
