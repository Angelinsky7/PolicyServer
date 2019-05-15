using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class Client {

        public Int32 Id { get; set; }
        public Boolean Enabled { get; set; } = true;
        public String ClientId { get; set; }
        //public ICollection<Secret> Secrets { get; set; } = new HashSet<Secret>();
        //public Boolean RequireClientSecret { get; set; } = true;
        public String ClientName { get; set; }
        public String Description { get; set; }
        public String ClientUri { get; set; }
        //public ICollection<String> AllowedScopes { get; set; } = new HashSet<String>();

        public Policy Policy { get; set; }

    }
}
