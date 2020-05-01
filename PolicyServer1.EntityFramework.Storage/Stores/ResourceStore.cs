using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.Models;
using PolicyServer1.Stores;

namespace PolicyServer1.EntityFramework.Storage.Stores {
    public class ResourceStore : IResourceStore {
        public Task<Guid> CreateAsync(Resource item) {
            throw new NotImplementedException();
        }

        public IQueryable<Resource> Query() {
            throw new NotImplementedException();
        }

        public Task<Resource> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Resource item) {
            throw new NotImplementedException();
        }
    }
}
