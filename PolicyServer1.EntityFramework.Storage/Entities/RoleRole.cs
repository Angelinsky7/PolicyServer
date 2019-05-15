using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class RoleRole {

        public Int32 RoleId { get; set; }
        public Role Role { get; set; }

        public Int32 ParentId { get; set; }
        public Role Parent { get; set; }

    }
}
