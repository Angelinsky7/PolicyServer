using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public interface IPermissionStore : IStore<Permission, Guid> {

        //TODO(demarco): Maybe we need to set it back
        //Task<Permission> GetByNameAsync(String name);

    }
}
