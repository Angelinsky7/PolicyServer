using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class RolePolicy : Policy {

        public ICollection<MmRolePolicyRole> Roles { get; set; } = new HashSet<MmRolePolicyRole>();

    }
}
