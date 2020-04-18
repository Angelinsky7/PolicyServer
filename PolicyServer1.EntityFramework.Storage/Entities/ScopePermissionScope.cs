using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ScopePermissionScope {

        public Guid ClientId { get; set; }
        public ScopePermission Client { get; set; }

        public Guid ScopeId { get; set; }
        public Scope Scope { get; set; }

    }
}
