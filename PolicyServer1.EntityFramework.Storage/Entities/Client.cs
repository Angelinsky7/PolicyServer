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

        public ICollection<MmClientResource> Resources { get; set; } = new HashSet<MmClientResource>();
        public ICollection<MmClientScope> Scopes { get; set; } = new HashSet<MmClientScope>();
        public ICollection<MmClientRole> Roles { get; set; } = new HashSet<MmClientRole>();
        public ICollection<MmClientPolicy> Policies { get; set; } = new HashSet<MmClientPolicy>();
        public ICollection<MmClientPermission> Permissions { get; set; } = new HashSet<MmClientPermission>();

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
