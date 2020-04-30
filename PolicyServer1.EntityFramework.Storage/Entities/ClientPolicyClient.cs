using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientPolicyClient {

        public Int64 Id { get; set; }

        public Guid ClientPolicyId { get; set; }
        public ClientPolicy ClientPolicy { get; set; }

        public String Client { get; set; }

    }
}
