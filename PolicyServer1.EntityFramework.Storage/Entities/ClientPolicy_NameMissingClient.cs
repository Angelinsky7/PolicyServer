using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientPolicy_NameMissingClient {

        public Int64 Id { get; set; }

        public Guid ClientPolicyId { get; set; }
        public ClientPolicy_NameMissing ClientPolicy { get; set; }

        public String Client { get; set; }

    }
}
