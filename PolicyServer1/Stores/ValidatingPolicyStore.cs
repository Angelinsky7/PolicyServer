using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PolicyServer1.Models;
using PolicyServer1.Validations;

namespace PolicyServer1.Stores {
    public class ValidatingPolicyStore<T> : IPolicyStore where T : IPolicyStore {

        private readonly T _inner;
        private readonly IPolicyConfigurationValidator _validator;
        private readonly ILogger<ValidatingPolicyStore<T>> _logger;
        private readonly String _validatorType;

        public ValidatingPolicyStore(
            T inner,
            IPolicyConfigurationValidator validator,
            ILogger<ValidatingPolicyStore<T>> logger
        ) {
            _inner = inner;
            _validator = validator;
            _logger = logger;
            _validatorType = _validator.GetType().FullName;
        }

        public Task<Policy> CreateAsync(Policy item) {
            throw new NotImplementedException();
        }

        public Task<Policy> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public IQueryable<Policy> Get() {
            throw new NotImplementedException();
        }

        public Task<Policy> RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Policy> UpdateAsync(Guid id, Policy item) {
            throw new NotImplementedException();
        }

        //public Task<Int32> CreateAsync(Policy newPolicy) {
        //    return _inner.CreateAsync(newPolicy);
        //}

        //public Task<Policy> GetAsync(Int32 policyId) {
        //    return _inner.GetAsync(policyId);
        //}

        //public Task RemoveAsync(Int32 policyId) {
        //    return _inner.RemoveAsync(policyId);
        //}

        //public Task UpdateAsync(Int32 policyId, Policy policy) {
        //    return _inner.UpdateAsync(policyId, policy);
        //}
    }
}
