using System;
using System.Collections.Generic;
using System.Text;
using PolicyServer1.Models;

namespace PolicyServer1.EntityFramework.Storage.Entities {

    public abstract class Policy {

        public Guid Id { get; set; } = Guid.NewGuid();

        public String Name { get; set; }
        public String Description { get; set; }

        public PolicyLogic Logic { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
