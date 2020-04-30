using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ScopePermission {

        public Guid Id { get; set; }
        public Permission Base { get; set; }

        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; }

        public ICollection<MmScopePermissionScope> Scopes { get; set; } = new HashSet<MmScopePermissionScope>();

    }
}
