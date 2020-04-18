using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class PolicyRole {

        public Int32 PolicyId { get; set; }
        public Policy Policy { get; set; }

        public Int32 RoleId { get; set; }
        public Role Role { get; set; }

    }
}
