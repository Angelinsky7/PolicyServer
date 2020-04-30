using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PolicyServer1.EntityFramework.Storage.Options {
    public class clientStoreOptions {

        public Action<DbContextOptionsBuilder> ConfigureDbContext { get; set; }
        public Action<IServiceProvider, DbContextOptionsBuilder> ResolveDbContextOptions { get; set; }
        public String DefaultSchema { get; set; } = null;

        public TableConfiguration Secret { get; set; } = new TableConfiguration("Secret");
        public TableConfiguration Scope { get; set; } = new TableConfiguration("Scope");
        public TableConfiguration Role { get; set; } = new TableConfiguration("Role");
        public TableConfiguration MmRoleRole { get; set; } = new TableConfiguration("MmRoleRole");
        public TableConfiguration Policy { get; set; } = new TableConfiguration("Policy");
        public TableConfiguration RolePolicy { get; set; } = new TableConfiguration("RolePolicy");
        public TableConfiguration MmRolePolicyRole { get; set; } = new TableConfiguration("MmRolePolicyRole");
        public TableConfiguration TimePolicy { get; set; } = new TableConfiguration("TimePolicy");
        public TableConfiguration UserPolicy { get; set; } = new TableConfiguration("UserPolicy");
        public TableConfiguration UserPolicyUser { get; set; } = new TableConfiguration("UserPolicyUser");
        public TableConfiguration GroupPolicy { get; set; } = new TableConfiguration("GroupPolicy");
        public TableConfiguration GroupPolicyGroup { get; set; } = new TableConfiguration("GroupPolicyGroup");
        public TableConfiguration ClientPolicy { get; set; } = new TableConfiguration("ClientPolicy");
        public TableConfiguration ClientPolicyClient { get; set; } = new TableConfiguration("ClientPolicyClient");
        public TableConfiguration AggregatedPolicy { get; set; } = new TableConfiguration("AggregatedPolicy");
        public TableConfiguration MmAggregatedPolicyPolicy { get; set; } = new TableConfiguration("MmAggregatedPolicyPolicy");
        public TableConfiguration Resource { get; set; } = new TableConfiguration("Resource");
        public TableConfiguration ResourceUri { get; set; } = new TableConfiguration("ResourceUri");
        public TableConfiguration MmResourceScope { get; set; } = new TableConfiguration("MmResourceScope");
        public TableConfiguration Permission { get; set; } = new TableConfiguration("Permission");
        public TableConfiguration MmPermissionPolicy { get; set; } = new TableConfiguration("MmPermissionPolicy");
        public TableConfiguration ScopePermission { get; set; } = new TableConfiguration("ScopePermission");
        public TableConfiguration MmScopePermissionScope { get; set; } = new TableConfiguration("MmScopePermissionScope");
        public TableConfiguration ResourcePermission { get; set; } = new TableConfiguration("ResourcePermission");
        public TableConfiguration Client { get; set; } = new TableConfiguration("Client");
        public TableConfiguration MmClientSecret { get; set; } = new TableConfiguration("MmClientSecret");
        public TableConfiguration MmClientResource { get; set; } = new TableConfiguration("MmClientResource");
        public TableConfiguration MmClientScope { get; set; } = new TableConfiguration("MmClientScope");
        public TableConfiguration MmClientRole { get; set; } = new TableConfiguration("MmClientRole");
        public TableConfiguration MmClientPolicy { get; set; } = new TableConfiguration("MmClientPolicy");
        public TableConfiguration MmClientPermission { get; set; } = new TableConfiguration("MmClientPermission");

    }
}
