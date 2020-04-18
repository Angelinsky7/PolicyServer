using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class RolePolicyRole {

        public Guid RolePolicyId { get; set; }
        public RolePolicy RolePolicy { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

    }
}
