using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class RolePermission {

        public Int32 RoleId { get; set; }
        public Role Role { get; set; }

        public Int32 PermissionId { get; set; }
        public Permission Permission { get; set; }

        public Boolean IsRevoked { get; set; } = false;

    }
}
