using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientPolicy : Policy {

        public ICollection<ClientPolicyClient> Clients { get; set; } = new HashSet<ClientPolicyClient>();

    }
}
