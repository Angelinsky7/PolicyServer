﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Secret {

        public Int64 Id { get; set; }
        public String Description { get; set; }
        public String Value { get; set; }
        public DateTime? Expiration { get; set; }
        public String Type { get; set; } = "SharedSecret";

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
