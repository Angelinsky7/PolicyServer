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

        public Task<Guid> CreateAsync(Permission item) => _inner.CreateAsync(item);
        public Task<Permission> GetAsync(Guid id) => _inner.GetAsync(id);
        public IQueryable<Permission> Query() => _inner.Query();
        public Task RemoveAsync(Guid id) => _inner.RemoveAsync(id);
        public Task UpdateAsync(Guid id, Permission item) => _inner.UpdateAsync(id, item);

    }
}
