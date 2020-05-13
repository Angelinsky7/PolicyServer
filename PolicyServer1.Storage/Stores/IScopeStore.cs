using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public interface IScopeStore : IStore<Scope, Guid> {    }
}
