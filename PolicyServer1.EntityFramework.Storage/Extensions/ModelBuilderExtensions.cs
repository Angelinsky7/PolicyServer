using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolicyServer1.EntityFramework.Storage.Entities;
using PolicyServer1.EntityFramework.Storage.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Extensions {
    public static class ModelBuilderExtensions {

        public static void ConfigurePolicyContext(this ModelBuilder modelBuilder, clientStoreOptions storeOptions) {
            if (!String.IsNullOrWhiteSpace(storeOptions.DefaultSchema)) { modelBuilder.HasDefaultSchema(storeOptions.DefaultSchema); }

            modelBuilder.Entity<Client>(client => {
                client.ToTable(storeOptions.Client);
                client.HasKey(p => p.Id);

                client.Property(p => p.ClientId).HasMaxLength(200).IsRequired();
                client.Property(p => p.ClientName).HasMaxLength(200);
                client.Property(p => p.Created).IsRequired();

                client.HasIndex(p => p.ClientId).IsUnique();

                //client.HasMany(p => p.AllowedScopes).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                //client.HasMany(p => p.Secrets).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasOne(p => p.Policy).WithOne().HasForeignKey<Client>(p => p.PolicyId);
            });

            modelBuilder.Entity<Policy>(policy => {
                policy.ToTable(storeOptions.Policy);
                policy.HasKey(p => p.Id);

                policy.Property(p => p.Created).IsRequired();
                //policy.Property(p => p)
                //policy.HasOne(p => p.Client).WithMany(p => p.Policies).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                policy.HasMany(p => p.Roles).WithOne(p => p.Policy).HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.Cascade);
                policy.HasMany(p => p.Permissions).WithOne(p => p.Policy).HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Role>(role => {
                role.ToTable(storeOptions.Role);
                role.HasKey(p => p.Id);

                role.Property(p => p.Name).HasMaxLength(200).IsRequired();
                role.Property(p => p.Created).IsRequired();

                role.HasIndex(p => p.Name).IsUnique();

                role.HasMany(p => p.Parents).WithOne().OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Permission>(permission => {
                permission.ToTable(storeOptions.Permission);
                permission.HasKey(p => p.Id);

                permission.HasIndex(p => new { p.PolicyId, p.Name }).IsUnique();

                permission.Property(p => p.Name).HasMaxLength(200).IsRequired();
                permission.Property(p => p.Description).HasMaxLength(200).IsRequired();
                permission.Property(p => p.Created).IsRequired();
            });

            // ---

            modelBuilder.Entity<RoleRole>(roleRole => {
                roleRole.ToTable(storeOptions.RoleRole);
                roleRole.HasKey(p => new { p.RoleId, p.ParentId});

                roleRole.HasIndex(p => new { p.RoleId, p.ParentId }).IsUnique();

                roleRole.HasOne(p => p.Role).WithMany(p => p.Parents).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Cascade);
                roleRole.HasOne(p => p.Parent).WithMany().HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RolePermission>(rolePermission => {
                rolePermission.ToTable(storeOptions.RolePermission);
                rolePermission.HasKey(p => new { p.RoleId, p.PermissionId });

                rolePermission.HasIndex(p => new { p.RoleId, p.PermissionId}).IsUnique();

                rolePermission.HasOne(p => p.Role).WithMany(p => p.Permissions).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Cascade);
                rolePermission.HasOne(p => p.Permission).WithMany().HasForeignKey(p => p.PermissionId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RoleIdentity>(roleIdentity => {
                roleIdentity.ToTable(storeOptions.RoleIdentity);
                roleIdentity.Property(p => p.Identity).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<RoleSubject>(roleSubject => {
                roleSubject.ToTable(storeOptions.RoleSubject);
                roleSubject.Property(p => p.Subject).HasMaxLength(200).IsRequired();
            });

        }

        private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration) where TEntity : class {
            return String.IsNullOrWhiteSpace(configuration.Schema) ? entityTypeBuilder.ToTable(configuration.Name) : entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
        }

    }
}
