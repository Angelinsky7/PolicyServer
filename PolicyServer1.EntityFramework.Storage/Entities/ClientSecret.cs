using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {

    [Obsolete]
    public class ClientSecret : Secret {

        public Int32 ClientId { get; set; }
        public Client Client { get; set; }

    }
}
