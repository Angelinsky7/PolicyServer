using Microsoft.Extensions.Logging;
using PolicyServer1.Models;
using PolicyServer1.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public class ValidatingScopeStore<T> : IScopeStore where T : IScopeStore {

        private readonly T _inner;
        private readonly IScopeConfigurationValidator _validator;
        private readonly ILogger<ValidatingScopeStore<T>> _logger;
        private readonly String _validatorType;

        public ValidatingScopeStore(
            T inner,
            IScopeConfigurationValidator validator,
            ILogger<ValidatingScopeStore<T>> logger
        ) {
            _inner = inner;
            _validator = validator;
            _logger = logger;
            _validatorType = _validator.GetType().FullName;
        }

        public Task<Guid> CreateAsync(Scope item) => _inner.CreateAsync(item);
        public Task<Scope> GetAsync(Guid id) => _inner.GetAsync(id);
        public IQueryable<Scope> Query() => _inner.Query();
        public Task RemoveAsync(Guid id) => _inner.RemoveAsync(id);
        public Task UpdateAsync(Guid id, Scope item) => _inner.UpdateAsync(id, item);

    }
}
