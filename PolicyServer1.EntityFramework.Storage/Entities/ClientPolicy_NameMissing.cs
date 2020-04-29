using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientPolicy_NameMissing {

        public Guid Id { get; set; }
        public Policy Base { get; set; }

        public ICollection<ClientPolicy_NameMissingClient> Users { get; set; } = new HashSet<ClientPolicy_NameMissingClient>();

    }
}
