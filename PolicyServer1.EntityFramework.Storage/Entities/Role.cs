using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {

    public class Role {

        public Guid Id { get; set; } = Guid.NewGuid();

        public String Name { get; set; }
        public String Description { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }
        //public Boolean NonEditable { get; set; }

        public ICollection<RoleRole> Parents { get; set; } = new HashSet<RoleRole>();

    }
}
