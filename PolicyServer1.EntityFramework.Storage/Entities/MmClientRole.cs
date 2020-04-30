using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class MmClientRole {

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

    }
}
