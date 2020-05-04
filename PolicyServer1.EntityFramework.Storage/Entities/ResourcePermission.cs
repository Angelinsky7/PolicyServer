using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ResourcePermission : Permission {

        public String ResouceType { get; set; }

        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; }

    }
}
