using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class RoleIdentity {
        public Int32 Id { get; set; }
        public String Identity { get; set; }

        public Int32 RoleId { get; set; }
        public Role Role { get; set; }
    }
}
