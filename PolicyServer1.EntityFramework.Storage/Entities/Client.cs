using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Client {

        public Guid Id { get; set; } = Guid.NewGuid();

        public Boolean Enabled { get; set; } = true;
        public String ClientId { get; set; }
        public ICollection<ClientSecret> Secrets { get; set; } = new HashSet<ClientSecret>();
        public Boolean RequireClientSecret { get; set; } = true;
        public String ClientName { get; set; }
        public String Description { get; set; }
        public String ClientUri { get; set; }

        public Models.PolicyEnforcement PolicyEnforcement { get; set; } = Models.PolicyEnforcement.Enforcing;
        public Models.DecisionStrategy DecisionStrategy { get; set; } = Models.DecisionStrategy.Affirmative;
        public Boolean AnalyseModeEnabled { get; set; } = false;
        public String PermissionSplitter { get; set; } = "#";

        public ICollection<ClientResource> Resources { get; set; } = new HashSet<ClientResource>();
        public ICollection<ClientScope> Scopes { get; set; } = new HashSet<ClientScope>();
        public ICollection<ClientRole> Roles { get; set; } = new HashSet<ClientRole>();
        public ICollection<ClientPolicy> Policies { get; set; } = new HashSet<ClientPolicy>();
        public ICollection<ClientPermission> Permissions { get; set; } = new HashSet<ClientPermission>();

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
