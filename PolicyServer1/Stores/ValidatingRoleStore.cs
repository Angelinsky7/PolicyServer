using Microsoft.Extensions.Logging;
using PolicyServer1.Models;
using PolicyServer1.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public class ValidatingRoleStore<T> : IRoleStore where T : IRoleStore {

        private readonly T _inner;
        private readonly IRoleConfigurationValidator _validator;
        private readonly ILogger<ValidatingRoleStore<T>> _logger;
        private readonly String _validatorType;

        public ValidatingRoleStore(
            T inner,
            IRoleConfigurationValidator validator,
            ILogger<ValidatingRoleStore<T>> logger
        ) {
            _inner = inner;
            _validator = validator;
            _logger = logger;
            _validatorType = _validator.GetType().FullName;
        }

        public Task<Guid> CreateAsync(Role item) {
            throw new NotImplementedException();
        }

        public Task<Role> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public IQueryable<Role> Query() {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Role item) {
            throw new NotImplementedException();
        }

        //public Task<Int32> CreateAsync(PolicyRole newRole) {
        //    return _inner.CreateAsync(newRole);
        //}

        //public Task<PolicyRole> GetAsync((Int32 policyId, Int32 roleId) key) {
        //    return _inner.GetAsync(key);
        //}

        //public Task RemoveAsync((Int32 policyId, Int32 roleId) key) {
        //    return _inner.RemoveAsync(key);
        //}

        //public Task UpdateAsync((Int32 policyId, Int32 roleId) key, PolicyRole Role) {
        //    return _inner.UpdateAsync(key, Role);
        //}
    }
}
