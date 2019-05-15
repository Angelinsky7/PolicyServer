using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Client {

        public Int32 Id { get; set; }
        public Boolean Enabled { get; set; } = true;
        public String ClientId { get; set; }
        //public ICollection<ClientSecret> Secrets { get; set; } = new HashSet<ClientSecret>();
        //public Boolean RequireClientSecret { get; set; } = true;
        public String ClientName { get; set; }
        public String Description { get; set; }
        public String ClientUri { get; set; }
        //public ICollection<ClientScope> AllowedScopes { get; set; } = new HashSet<ClientScope>();

        public Nullable<Int32> PolicyId { get; set; }
        public Policy Policy { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
