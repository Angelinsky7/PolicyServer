using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class Client {

        public Guid Id { get; set; } = Guid.NewGuid();
        //public Int32 Id { get; set; }
        public Boolean Enabled { get; set; } = true;
        public String ClientId { get; set; }
        public ICollection<Secret> Secrets { get; set; } = new HashSet<Secret>();
        public Boolean RequireClientSecret { get; set; } = true;
        public String ClientName { get; set; }
        public String Description { get; set; }
        public String ClientUri { get; set; }
        //public ICollection<String> AllowedScopes { get; set; } = new HashSet<String>();

        //public Policy Policy { get; set; }

        public ClientOption Options { get; set; } = new ClientOption();

        public ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();
        public ICollection<Scope> Scopes { get; set; } = new HashSet<Scope>();
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public ICollection<Policy> Policies { get; set; } = new HashSet<Policy>();
        public ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();

    }
}
