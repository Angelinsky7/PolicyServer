using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ScopePermission : Permission {

        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; }

        public ICollection<MmScopePermissionScope> Scopes { get; set; } = new HashSet<MmScopePermissionScope>();

    }
}
