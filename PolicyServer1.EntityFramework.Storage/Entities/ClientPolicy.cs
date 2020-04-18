using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientPolicy {

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid PolicyId { get; set; }
        public Policy Policy { get; set; }

    }
}
