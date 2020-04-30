using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class MmRoleRole {

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Guid ParentId { get; set; }
        public Role Parent{ get; set; }

    }
}
