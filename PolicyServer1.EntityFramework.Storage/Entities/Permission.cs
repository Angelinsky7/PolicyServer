using System;
using System.Collections.Generic;
using System.Text;
using PolicyServer1.Models;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Permission {

        public Guid Id { get; set; } = Guid.NewGuid();

        public String Name { get; set; }
        public String Description { get; set; }

        public DecisionStrategy DecisionStrategy { get; set; }

        public ICollection<MmPermissionPolicy> Policies { get; set; } = new HashSet<MmPermissionPolicy>();

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
