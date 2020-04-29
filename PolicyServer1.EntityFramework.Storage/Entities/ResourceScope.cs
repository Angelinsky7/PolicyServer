using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ResourceScope {

        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; }

        public Guid ScopeId { get; set; }
        public Scope Scope { get; set; }

    }
}
