using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PolicyServer1.EntityFramework.Storage.Options {
    public class clientStoreOptions {

        public Action<DbContextOptionsBuilder> ConfigureDbContext { get; set; }
        public Action<IServiceProvider, DbContextOptionsBuilder> ResolveDbContextOptions { get; set; }
        public String DefaultSchema { get; set; } = null;

        
        
      
        public TableConfiguration Permission { get; set; } = new TableConfiguration("Permission");

        //public TableConfiguration PolicyRole { get; set; } = new TableConfiguration("PolicyRole");        
        //public TableConfiguration RoleIdentity { get; set; } = new TableConfiguration("RoleIdentity");
        //public TableConfiguration RoleSubject { get; set; } = new TableConfiguration("RoleSubject");
        //public TableConfiguration RolePermission { get; set; } = new TableConfiguration("RolePermission");



        public TableConfiguration Secret { get; set; } = new TableConfiguration("Secret");
        public TableConfiguration Scope { get; set; } = new TableConfiguration("Scope");
        public TableConfiguration Role { get; set; } = new TableConfiguration("Role");
        public TableConfiguration RoleRole { get; set; } = new TableConfiguration("RoleRole");
        public TableConfiguration Policy { get; set; } = new TableConfiguration("Policy");
        public TableConfiguration RolePolicy { get; set; } = new TableConfiguration("RolePolicy");
        public TableConfiguration RolePolicyRole { get; set; } = new TableConfiguration("RolePolicyRole");
        public TableConfiguration TimePolicy { get; set; } = new TableConfiguration("TimePolicy");
        public TableConfiguration UserPolicy { get; set; } = new TableConfiguration("UserPolicy");
        public TableConfiguration UserPolicyUser { get; set; } = new TableConfiguration("UserPolicyUser");
        public TableConfiguration GroupPolicy { get; set; } = new TableConfiguration("GroupPolicy");
        public TableConfiguration GroupPolicyGroup { get; set; } = new TableConfiguration("GroupPolicyGroup");
        public TableConfiguration ClientPolicy_NameMissing { get; set; } = new TableConfiguration("ClientPolicy_NameMissing");
        public TableConfiguration ClientPolicy_NameMissingClient { get; set; } = new TableConfiguration("ClientPolicy_NameMissingClient");
        public TableConfiguration AggregatedPolicy { get; set; } = new TableConfiguration("AggregatedPolicy");
        public TableConfiguration AggregatedPolicyPolicy { get; set; } = new TableConfiguration("AggregatedPolicyPolicy");
        public TableConfiguration Resource { get; set; } = new TableConfiguration("Resource");
        public TableConfiguration ResourceUri { get; set; } = new TableConfiguration("ResourceUri");
        public TableConfiguration ResourceScope { get; set; } = new TableConfiguration("ResourceScope");
        public TableConfiguration Client { get; set; } = new TableConfiguration("Client");
        public TableConfiguration ClientSecret { get; set; } = new TableConfiguration("ClientSecret");
        public TableConfiguration ClientResource { get; set; } = new TableConfiguration("ClientResource");
        public TableConfiguration ClientScope { get; set; } = new TableConfiguration("ClientScope");
        public TableConfiguration ClientRole { get; set; } = new TableConfiguration("ClientRole");
        public TableConfiguration ClientPolicy { get; set; } = new TableConfiguration("ClientPolicy");
        public TableConfiguration ClientPermission { get; set; } = new TableConfiguration("ClientPermission");

    }
}
