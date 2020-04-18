using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Scope {

        public Guid Id { get; set; } = Guid.NewGuid();

        public String Name { get; set; }
        public String DisplayName { get; set; }
        public String IconUri { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }
        //public Boolean NonEditable { get; set; }

    }
}
