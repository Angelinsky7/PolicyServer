using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Resource {

        public Guid Id { get; set; } = Guid.NewGuid();

        public String Name { get; set; }
        public String DisplayName { get; set; }
        public String Type { get; set; }
        public ICollection<ResourceUri> Uri { get; set; } = new HashSet<ResourceUri>();
        public ICollection<MmResourceScope> Scopes { get; set; } = new HashSet<MmResourceScope>();
        public String IconUri { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
