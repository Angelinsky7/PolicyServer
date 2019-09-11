using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class Resource {

        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public String DisplayName { get; set; }
        public String Type { get; set; }
        public ICollection<Uri> Uri { get; set; } = new HashSet<Uri>();
        public ICollection<Scope> Scopes { get; set; } = new HashSet<Scope>();
        public Uri IconUri { get; set; }

        //public Boolean UserManageAccessEnabled { get; set; }
        //public Dictionary<String, Object> Attributes { get; set; }

    }
}
