using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {

    [Obsolete]
    public class ClientScope {

        public Int32 Id { get; set; }
        public String Scope { get; set; }

        public Int32 ClientId { get; set; }
        public Client Client { get; set; }

    }
}
