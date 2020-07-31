using Microsoft.Extensions.Logging;
using PolicyServer1.Models;
using PolicyServer1.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public class ValidatingResourceStore<T> : IResourceStore where T : IResourceStore {

        private readonly T _inner;
        private readonly IResourceConfigurationValidator _validator;
        private readonly ILogger<ValidatingResourceStore<T>> _logger;
        private readonly String _validatorType;

        public ValidatingResourceStore(
            T inner,
            IResourceConfigurationValidator validator,
            ILogger<ValidatingResourceStore<T>> logger
        ) {
            _inner = inner;
            _validator = validator;
            _logger = logger;
            _validatorType = _validator.GetType().FullName;
        }

        public Task<Guid> CreateAsync(Resource item) => _inner.CreateAsync(item);
        public Task<Resource> GetAsync(Guid id) => _inner.GetAsync(id);
        public IQueryable<Resource> Query() => _inner.Query();
        public Task RemoveAsync(Guid id) => _inner.RemoveAsync(id);
        public Task UpdateAsync(Guid id, Resource item) => _inner.UpdateAsync(id, item);

    }
}
