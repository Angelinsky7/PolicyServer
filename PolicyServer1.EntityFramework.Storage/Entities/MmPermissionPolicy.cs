using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class MmPermissionPolicy {

        public Guid PermissionId{ get; set; }
        public Permission Permission { get; set; }

        public Guid PolicyId { get; set; }
        public Policy Policy { get; set; }

    }
}
