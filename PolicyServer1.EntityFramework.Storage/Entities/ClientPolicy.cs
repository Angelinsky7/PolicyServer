using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientPolicy {

        public Guid Id { get; set; }
        public Policy Base { get; set; }

        public ICollection<ClientPolicyClient> Users { get; set; } = new HashSet<ClientPolicyClient>();

    }
}
