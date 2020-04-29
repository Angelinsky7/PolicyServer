using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientSecret {

        public Int64 SecretId { get; set; }
        public Secret Secret { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        
    }
}
