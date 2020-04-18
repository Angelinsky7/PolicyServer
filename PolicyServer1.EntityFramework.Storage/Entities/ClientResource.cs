using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientResource {

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid RessourceId { get; set; }
        public Resource Ressource { get; set; }

    }
}
