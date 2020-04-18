using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Role {

        public Int32 Id { get; set; }

        //public Boolean Enabled { get; set; } = true;
        public String Name { get; set; }
        public String Description { get; set; }

        public Int32 PolicyId { get; set; }
        public Policy Policy { get; set; }

        public ICollection<RoleRole> Parents { get; internal set; } = new HashSet<RoleRole>();
        public ICollection<RolePermission> Permissions { get; internal set; } = new HashSet<RolePermission>();
        public ICollection<RoleIdentity> IdentityRoles { get; internal set; } = new HashSet<RoleIdentity>();
        public ICollection<RoleSubject> Subjects { get; internal set; } = new HashSet<RoleSubject>();

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
