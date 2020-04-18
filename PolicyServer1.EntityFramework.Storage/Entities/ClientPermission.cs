using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientPermission {

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }

    }
}
