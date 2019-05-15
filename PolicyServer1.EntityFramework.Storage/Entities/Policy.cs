using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Policy {

        public Int32 Id { get; set; }

        public String Hash { get; set; }
        public DateTime LastPolicyChangeDate { get; set; }

        public ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
