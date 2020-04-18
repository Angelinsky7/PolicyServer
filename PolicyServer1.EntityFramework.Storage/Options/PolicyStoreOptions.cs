﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PolicyServer1.EntityFramework.Storage.Options {
    public class clientStoreOptions {

        public Action<DbContextOptionsBuilder> ConfigureDbContext { get; set; }
        public Action<IServiceProvider, DbContextOptionsBuilder> ResolveDbContextOptions { get; set; }
        public String DefaultSchema { get; set; } = null;

        public TableConfiguration Client { get; set; } = new TableConfiguration("Client");
        
      
        public TableConfiguration Permission { get; set; } = new TableConfiguration("Permission");

        public TableConfiguration PolicyRole { get; set; } = new TableConfiguration("PolicyRole");

        
        public TableConfiguration RoleIdentity { get; set; } = new TableConfiguration("RoleIdentity");
        public TableConfiguration RoleSubject { get; set; } = new TableConfiguration("RoleSubject");
        public TableConfiguration RolePermission { get; set; } = new TableConfiguration("RolePermission");
        
        
        
        
        public TableConfiguration Scope { get; set; } = new TableConfiguration("Scope");
        public TableConfiguration Role { get; set; } = new TableConfiguration("Role");
        public TableConfiguration RoleRole { get; set; } = new TableConfiguration("RoleRole");
        public TableConfiguration Policy { get; set; } = new TableConfiguration("Policy");
        public TableConfiguration RolePolicy { get; set; } = new TableConfiguration("RolePolicy");
        public TableConfiguration RolePolicyRole { get; set; } = new TableConfiguration("RolePolicyRole");
    }
}
