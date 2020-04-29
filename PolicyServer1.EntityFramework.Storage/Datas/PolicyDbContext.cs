using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PolicyServer1.EntityFramework.Storage.Entities;
using PolicyServer1.EntityFramework.Storage.Extensions;
using PolicyServer1.EntityFramework.Storage.Interfaces;
using PolicyServer1.EntityFramework.Storage.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.EntityFramework.Storage.Datas {
    public class PolicyDbContext : PolicyDbContext<PolicyDbContext> {
        public PolicyDbContext(DbContextOptions<PolicyDbContext> options, clientStoreOptions storeOptions) : base(options, storeOptions) { }
    }

    public class PolicyDbContext<TContext> : DbContext, IPolicyDbContext where TContext : DbContext, IPolicyDbContext {

        private readonly clientStoreOptions _storeOptions;

        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Client> Clients { get; set; }

        public PolicyDbContext(DbContextOptions<TContext> options, clientStoreOptions storeOptions) : base(options) {
            _storeOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
        }

        public Task<Int32> SaveChangesAsync() {
            return base.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction() {
            return Database.BeginTransaction();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync() {
            return Database.BeginTransactionAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ConfigurePolicyContext(_storeOptions);

            base.OnModelCreating(modelBuilder);
        }

    }

}
