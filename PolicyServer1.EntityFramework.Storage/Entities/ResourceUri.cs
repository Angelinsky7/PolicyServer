using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ResourceUri {

        public Int64 Id { get; set; }

        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; }

        public String Uri { get; set; }

    }
}
