using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class RolePolicy {

        public Guid Id { get; set; }
        public Policy Base { get; set; }

        public ICollection<RolePolicyRole> Roles { get; set; } = new HashSet<RolePolicyRole>();

    }
}
