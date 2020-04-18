using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Permission {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        
        public Int32 PolicyId { get; set; }
        public Policy Policy { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
