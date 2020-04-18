﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class ClientSecret {

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Int64 BaseId { get; set; }
        public Int64 Base { get; set; }

    }
}
