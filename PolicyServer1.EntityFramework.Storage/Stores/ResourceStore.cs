using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.Models;
using PolicyServer1.Stores;

namespace PolicyServer1.EntityFramework.Storage.Stores {
    public class ResourceStore : IResourceStore {
        public Task<Resource> CreateAsync(Resource item) {
            throw new NotImplementedException();
        }

        public IQueryable<Resource> Get() {
            throw new NotImplementedException();
        }

        public Task<Resource> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Resource> RemoveAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Resource> UpdateAsync(Guid id, Resource item) {
            throw new NotImplementedException();
        }
    }
}
