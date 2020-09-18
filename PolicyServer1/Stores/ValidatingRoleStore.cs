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

        public Task<Guid> CreateAsync(Role item) => _inner.CreateAsync(item);
        public Task<Role> GetAsync(Guid id) => _inner.GetAsync(id);
        public IQueryable<Role> Query() => _inner.Query();
        public Task RemoveAsync(Guid id) => _inner.RemoveAsync(id);
        public Task UpdateAsync(Guid id, Role item) => _inner.UpdateAsync(id, item);

    }
}
