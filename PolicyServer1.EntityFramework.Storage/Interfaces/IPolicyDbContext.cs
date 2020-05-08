using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using PolicyServer1.EntityFramework.Storage.Entities;

namespace PolicyServer1.EntityFramework.Storage.Interfaces {
    public interface IPolicyDbContext : IDisposable {

        DbSet<Scope> Scopes { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Policy> Policies { get; set; }
        DbSet<Resource> Resources { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Permission> Permissions { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        ChangeTracker ChangeTracker { get; }

        Int32 SaveChanges();
        Task<Int32> SaveChangesAsync();

        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
