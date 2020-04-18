using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ResourcePermission {

        public Guid Id { get; set; }
        public Permission Base { get; set; }

        public String ResouceType { get; set; }
        public Resource Resource { get; set; }

    }
}
