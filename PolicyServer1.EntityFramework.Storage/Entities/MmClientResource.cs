using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class MmClientResource {

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; }

    }
}
