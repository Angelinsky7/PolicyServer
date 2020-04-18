﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientScope {

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid ScopeId{ get; set; }
        public Scope Scope { get; set; }

    }
}
