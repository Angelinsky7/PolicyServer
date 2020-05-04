using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientSecret : Secret {

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        
    }
}
