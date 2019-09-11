
using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Stores {
    public interface IResourceStore : IStore<Resource, Guid> {
    }
}
