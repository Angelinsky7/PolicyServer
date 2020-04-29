using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PolicyServer1.EntityFramework.Storage.Entities;
using PolicyServer1.EntityFramework.Storage.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Extensions {
    public static class ModelBuilderExtensions {

        public static void ConfigurePolicyContext(this ModelBuilder modelBuilder, clientStoreOptions storeOptions) {
            if (!String.IsNullOrWhiteSpace(storeOptions.DefaultSchema)) { modelBuilder.HasDefaultSchema(storeOptions.DefaultSchema); }

            modelBuilder.Entity<Scope>(scope => {
                scope.ToTable(storeOptions.Scope);
                scope.HasKey(p => p.Id);

                scope.Property(p => p.Name).HasMaxLength(200).IsRequired();
                scope.Property(p => p.DisplayName).HasMaxLength(200).IsRequired();
                scope.Property(p => p.IconUri).HasMaxLength(2000);

                scope.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                scope.Property(p => p.Updated).ValueGeneratedOnUpdate();
                scope.Property(p => p.LastAccessed);
            });

            modelBuilder.Entity<Role>(role => {
                role.ToTable(storeOptions.Role);
                role.HasKey(p => p.Id);

                role.Property(p => p.Name).HasMaxLength(200).IsRequired();
                role.Property(p => p.Description).HasMaxLength(200).IsRequired();

                role.HasMany(p => p.Parents).WithOne(p => p.Role).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Restrict);

                role.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                role.Property(p => p.Updated).ValueGeneratedOnUpdate();
                role.Property(p => p.LastAccessed);
            });

            modelBuilder.Entity<RoleRole>(roleRole => {
                roleRole.ToTable(storeOptions.RoleRole);
                roleRole.HasKey(p => new { p.RoleId, p.ParentId });

                roleRole.HasIndex(p => new { p.RoleId, p.ParentId }).IsUnique();

                roleRole.HasOne(p => p.Role).WithMany(p => p.Parents).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Cascade);
                roleRole.HasOne(p => p.Parent).WithMany().HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Policy>(policy => {
                policy.ToTable(storeOptions.Policy);
                policy.HasKey(p => p.Id);

                policy.Property(p => p.Name).HasMaxLength(200).IsRequired();
                policy.Property(p => p.Description).HasMaxLength(200).IsRequired();
                policy.Property(p => p.Logic).IsRequired();

                policy.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                policy.Property(p => p.Updated).ValueGeneratedOnUpdate();
                policy.Property(p => p.LastAccessed);
            });

            modelBuilder.Entity<RolePolicy>(rolePolicy => {
                rolePolicy.ToTable(storeOptions.RolePolicy);
                rolePolicy.HasKey(p => p.Id);

                rolePolicy.HasOne(p => p.Base).WithMany().HasForeignKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);
                rolePolicy.HasMany(p => p.Roles).WithOne(p => p.RolePolicy).HasForeignKey(p => p.RolePolicyId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RolePolicyRole>(rolePolicyRole => {
                rolePolicyRole.ToTable(storeOptions.RolePolicyRole);
                rolePolicyRole.HasKey(p => new { p.RolePolicyId, p.RoleId });

                rolePolicyRole.HasIndex(p => new { p.RolePolicyId, p.RoleId }).IsUnique();

                rolePolicyRole.HasOne(p => p.RolePolicy).WithMany(p => p.Roles).HasForeignKey(p => p.RolePolicyId).OnDelete(DeleteBehavior.Cascade);
                rolePolicyRole.HasOne(p => p.Role).WithMany().HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TimePolicy>(timePolicy => {
                timePolicy.ToTable(storeOptions.TimePolicy);
                timePolicy.HasKey(p => p.Id);

                timePolicy.HasOne(p => p.Base).WithOne().HasForeignKey<TimePolicy>(p => p.Id).OnDelete(DeleteBehavior.Cascade);
                timePolicy.HasOne(p => p.DayOfMonth).WithOne().HasForeignKey<TimePolicy>(p => p.DayOfMonthId).OnDelete(DeleteBehavior.SetNull);
                timePolicy.HasOne(p => p.Month).WithOne().HasForeignKey<TimePolicy>(p => p.MonthId).OnDelete(DeleteBehavior.SetNull);
                timePolicy.HasOne(p => p.Year).WithOne().HasForeignKey<TimePolicy>(p => p.YearId).OnDelete(DeleteBehavior.SetNull);
                timePolicy.HasOne(p => p.Hour).WithOne().HasForeignKey<TimePolicy>(p => p.HourId).OnDelete(DeleteBehavior.SetNull);
                timePolicy.HasOne(p => p.Minute).WithOne().HasForeignKey<TimePolicy>(p => p.MinuteId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserPolicy>(userPolicy => {
                userPolicy.ToTable(storeOptions.UserPolicy);
                userPolicy.HasKey(p => p.Id);

                userPolicy.HasOne(p => p.Base).WithOne().HasForeignKey<UserPolicy>(p => p.Id).OnDelete(DeleteBehavior.Cascade);
                userPolicy.HasMany(p => p.Users).WithOne(p => p.UserPolicy).HasForeignKey(p => p.UserPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserPolicyUser>(userPolicyUser => {
                userPolicyUser.ToTable(storeOptions.UserPolicyUser);
                userPolicyUser.HasKey(p => p.Id);

                userPolicyUser.HasOne(p => p.UserPolicy).WithMany(p => p.Users).HasForeignKey(p => p.UserPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GroupPolicy>(groupPolicy => {
                groupPolicy.ToTable(storeOptions.GroupPolicy);
                groupPolicy.HasKey(p => p.Id);

                groupPolicy.HasOne(p => p.Base).WithOne().HasForeignKey<GroupPolicy>(p => p.Id).OnDelete(DeleteBehavior.Cascade);
                groupPolicy.HasMany(p => p.Users).WithOne(p => p.GroupPolicy).HasForeignKey(p => p.GroupPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GroupPolicyGroup>(groupPolicyGroup => {
                groupPolicyGroup.ToTable(storeOptions.GroupPolicyGroup);
                groupPolicyGroup.HasKey(p => p.Id);

                groupPolicyGroup.HasOne(p => p.GroupPolicy).WithMany(p => p.Users).HasForeignKey(p => p.GroupPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ClientPolicy_NameMissing>(clientPolicy_NameMissing => {
                clientPolicy_NameMissing.ToTable(storeOptions.ClientPolicy_NameMissing);
                clientPolicy_NameMissing.HasKey(p => p.Id);

                clientPolicy_NameMissing.HasOne(p => p.Base).WithOne().HasForeignKey<ClientPolicy_NameMissing>(p => p.Id).OnDelete(DeleteBehavior.Cascade);
                clientPolicy_NameMissing.HasMany(p => p.Users).WithOne(p => p.ClientPolicy).HasForeignKey(p => p.ClientPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ClientPolicy_NameMissingClient>(clientPolicy_NameMissingClient => {
                clientPolicy_NameMissingClient.ToTable(storeOptions.ClientPolicy_NameMissingClient);
                clientPolicy_NameMissingClient.HasKey(p => p.Id);

                clientPolicy_NameMissingClient.HasOne(p => p.ClientPolicy).WithMany(p => p.Users).HasForeignKey(p => p.ClientPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AggregatedPolicy>(aggregatedPolicy => {
                aggregatedPolicy.ToTable(storeOptions.AggregatedPolicy);
                aggregatedPolicy.HasKey(p => p.Id);

                aggregatedPolicy.Property(p => p.DecisionStrategy).IsRequired();

                aggregatedPolicy.HasOne(p => p.Base).WithOne().HasForeignKey<AggregatedPolicy>(p => p.Id).OnDelete(DeleteBehavior.Cascade);
                aggregatedPolicy.HasMany(p => p.Policies).WithOne(p => p.AggregatedPolicy).HasForeignKey(p => p.AggregatedPolicyId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AggregatedPolicyPolicy>(aggregatedPolicyPolicy => {
                aggregatedPolicyPolicy.ToTable(storeOptions.AggregatedPolicyPolicy);
                aggregatedPolicyPolicy.HasKey(p => new { p.AggregatedPolicyId, p.PolicyId });

                aggregatedPolicyPolicy.HasIndex(p => new { p.AggregatedPolicyId, p.PolicyId }).IsUnique();

                aggregatedPolicyPolicy.HasOne(p => p.AggregatedPolicy).WithMany(p => p.Policies).HasForeignKey(p => p.AggregatedPolicyId).OnDelete(DeleteBehavior.Cascade);
                aggregatedPolicyPolicy.HasOne(p => p.Policy).WithMany().HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Resource>(resource => {
                resource.ToTable(storeOptions.Resource);
                resource.HasKey(p => p.Id);

                resource.HasIndex(p => p.Name).IsUnique();

                resource.Property(p => p.Name).HasMaxLength(200).IsRequired();
                resource.Property(p => p.DisplayName).HasMaxLength(200).IsRequired();
                resource.Property(p => p.Type).HasMaxLength(200);
                resource.Property(p => p.IconUri).HasMaxLength(2000);

                resource.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                resource.Property(p => p.Updated).ValueGeneratedOnUpdate();
                resource.Property(p => p.LastAccessed);

                resource.HasMany(p => p.Uri).WithOne(p => p.Resource).HasForeignKey(p => p.ResouceId).OnDelete(DeleteBehavior.Cascade);
                resource.HasMany(p => p.Scopes).WithOne(p => p.Resource).HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ResourceUri>(resourceUri => {
                resourceUri.ToTable(storeOptions.ResourceUri);
                resourceUri.HasKey(p => p.Id);

                resourceUri.HasOne(p => p.Resource).WithMany(p => p.Uri).HasForeignKey(p => p.ResouceId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ResourceScope>(resourceScope => {
                resourceScope.ToTable(storeOptions.ResourceScope);
                resourceScope.HasKey(p => new { p.ResourceId, p.ScopeId});

                resourceScope.HasIndex(p => new { p.ResourceId, p.ScopeId }).IsUnique();

                resourceScope.HasOne(p => p.Resource).WithMany(p => p.Scopes).HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.Cascade);
                resourceScope.HasOne(p => p.Scope).WithMany().HasForeignKey(p => p.ScopeId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Secret>(secret => {
                secret.ToTable(storeOptions.Secret);
                secret.HasKey(p => p.Id);

                secret.Property(p => p.Description).HasMaxLength(100);
                secret.Property(p => p.Value).HasMaxLength(100).IsRequired();
                secret.Property(p => p.Expiration);
                secret.Property(p => p.Type).HasMaxLength(100).IsRequired();

                secret.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                secret.Property(p => p.Updated).ValueGeneratedOnUpdate();
                secret.Property(p => p.LastAccessed);
            });

            modelBuilder.Entity<Permission>(permission => {
                permission.ToTable(storeOptions.Permission);
                permission.HasKey(p => p.Id);

                permission.Property(p => p.Name).HasMaxLength(200).IsRequired();
                permission.Property(p => p.Description).HasMaxLength(200);
                permission.Property(p => p.DecisionStrategy).IsRequired();

                permission.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                permission.Property(p => p.Updated).ValueGeneratedOnUpdate();
                permission.Property(p => p.LastAccessed);

                permission.HasMany(p => p.Policies).WithOne(p => p.Permission).HasForeignKey(p => p.PermissionId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PermissionPolicy>(permissionPolicy => {
                permissionPolicy.ToTable(storeOptions.PermissionPolicy);
                permissionPolicy.HasKey(p => new { p.PermissionId, p.PolicyId });

                permissionPolicy.HasIndex(p => new { p.PermissionId, p.PolicyId }).IsUnique();

                permissionPolicy.HasOne(p => p.Permission).WithMany(p => p.Policies).HasForeignKey(p => p.PermissionId).OnDelete(DeleteBehavior.Cascade);
                permissionPolicy.HasOne(p => p.Policy).WithMany().HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ScopePermission>(scopePermission => {
                scopePermission.ToTable(storeOptions.ScopePermission);
                scopePermission.HasKey(p => p.Id);

                scopePermission.HasOne(p => p.Base).WithOne().HasForeignKey<ScopePermission>(p => p.Id).OnDelete(DeleteBehavior.Cascade);
                scopePermission.HasOne(p => p.Resource).WithMany().HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.Cascade);
                scopePermission.HasMany(p => p.Scopes).WithOne(p => p.ScopePermission).HasForeignKey(p => p.ScopePermissionId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ScopePermissionScope>(scopePermissionScope => {
                scopePermissionScope.ToTable(storeOptions.ScopePermissionScope);
                scopePermissionScope.HasKey(p => new { p.ScopePermissionId, p.ScopeId });

                scopePermissionScope.HasIndex(p => new { p.ScopePermissionId, p.ScopeId }).IsUnique();

                scopePermissionScope.HasOne(p => p.ScopePermission).WithMany(p => p.Scopes).HasForeignKey(p => p.ScopePermissionId).OnDelete(DeleteBehavior.Cascade);
                scopePermissionScope.HasOne(p => p.Scope).WithMany().HasForeignKey(p => p.ScopeId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ResourcePermission>(resourcePermission => {
                resourcePermission.ToTable(storeOptions.ResourcePermission);
                resourcePermission.HasKey(p => p.Id);

                resourcePermission.Property(p => p.ResouceType).HasMaxLength(200).IsRequired();

                resourcePermission.HasOne(p => p.Base).WithOne().HasForeignKey<ResourcePermission>(p => p.Id).OnDelete(DeleteBehavior.Cascade);
                resourcePermission.HasOne(p => p.Resource).WithMany().HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ScopePermissionScope>(scopePermissionScope => {
                scopePermissionScope.ToTable(storeOptions.ScopePermissionScope);
                scopePermissionScope.HasKey(p => new { p.ScopePermissionId, p.ScopeId });

                scopePermissionScope.HasIndex(p => new { p.ScopePermissionId, p.ScopeId }).IsUnique();

                scopePermissionScope.HasOne(p => p.ScopePermission).WithMany(p => p.Scopes).HasForeignKey(p => p.ScopePermissionId).OnDelete(DeleteBehavior.Cascade);
                scopePermissionScope.HasOne(p => p.Scope).WithMany().HasForeignKey(p => p.ScopeId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Client>(client => {
                client.ToTable(storeOptions.Client);
                client.HasKey(p => p.Id);

                client.HasIndex(p => p.ClientId).IsUnique();

                client.Property(p => p.Enabled).IsRequired();
                client.Property(p => p.ClientId).HasMaxLength(200).IsRequired();
                client.Property(p => p.RequireClientSecret).IsRequired();
                client.Property(p => p.ClientName).HasMaxLength(200).IsRequired();
                client.Property(p => p.Description).HasMaxLength(200);
                client.Property(p => p.ClientUri).HasMaxLength(2000);

                client.Property(p => p.PolicyEnforcement).IsRequired();
                client.Property(p => p.DecisionStrategy).IsRequired();
                client.Property(p => p.AnalyseModeEnabled).IsRequired();
                client.Property(p => p.PermissionSplitter).HasMaxLength(2).IsRequired();

                client.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                client.Property(p => p.Updated).ValueGeneratedOnUpdate();
                client.Property(p => p.LastAccessed);

                client.HasMany(p => p.Secrets).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                client.HasMany(p => p.Resources).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                client.HasMany(p => p.Scopes).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                client.HasMany(p => p.Roles).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                client.HasMany(p => p.Policies).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                client.HasMany(p => p.Permissions).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ClientSecret>(clientSecret => {
                clientSecret.ToTable(storeOptions.ClientSecret);
                clientSecret.HasKey(p => new { p.ClientId, p.SecretId });

                clientSecret.HasIndex(p => new { p.ClientId, p.SecretId }).IsUnique();

                clientSecret.HasOne(p => p.Client).WithMany(p => p.Secrets).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientSecret.HasOne(p => p.Secret).WithMany().HasForeignKey(p => p.SecretId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ClientResource>(clientResource => {
                clientResource.ToTable(storeOptions.ClientResource);
                clientResource.HasKey(p => new { p.ClientId, p.ResourceId });

                clientResource.HasIndex(p => new { p.ClientId, p.ResourceId }).IsUnique();

                clientResource.HasOne(p => p.Client).WithMany(p => p.Resources).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientResource.HasOne(p => p.Resource).WithMany().HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ClientScope>(clientScope => {
                clientScope.ToTable(storeOptions.ClientScope);
                clientScope.HasKey(p => new { p.ClientId, p.ScopeId });

                clientScope.HasIndex(p => new { p.ClientId, p.ScopeId }).IsUnique();

                clientScope.HasOne(p => p.Client).WithMany(p => p.Scopes).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientScope.HasOne(p => p.Scope).WithMany().HasForeignKey(p => p.ScopeId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ClientRole>(clientRole => {
                clientRole.ToTable(storeOptions.ClientRole);
                clientRole.HasKey(p => new { p.ClientId, p.RoleId });

                clientRole.HasIndex(p => new { p.ClientId, p.RoleId }).IsUnique();

                clientRole.HasOne(p => p.Client).WithMany(p => p.Roles).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientRole.HasOne(p => p.Role).WithMany().HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ClientPolicy>(clientPolicy => {
                clientPolicy.ToTable(storeOptions.ClientPolicy);
                clientPolicy.HasKey(p => new { p.ClientId, p.PolicyId });

                clientPolicy.HasIndex(p => new { p.ClientId, p.PolicyId }).IsUnique();

                clientPolicy.HasOne(p => p.Client).WithMany(p => p.Policies).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientPolicy.HasOne(p => p.Policy).WithMany().HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ClientPermission>(clientPermission => {
                clientPermission.ToTable(storeOptions.ClientPermission);
                clientPermission.HasKey(p => new { p.ClientId, p.PermissionId });

                clientPermission.HasIndex(p => new { p.ClientId, p.PermissionId }).IsUnique();

                clientPermission.HasOne(p => p.Client).WithMany(p => p.Permissions).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientPermission.HasOne(p => p.Permission).WithMany().HasForeignKey(p => p.PermissionId).OnDelete(DeleteBehavior.Restrict);
            });












            //modelBuilder.Entity<Client>(client => {
            //    client.ToTable(storeOptions.Client);
            //    client.HasKey(p => p.Id);

            //    client.Property(p => p.ClientId).HasMaxLength(200).IsRequired();
            //    client.Property(p => p.ClientName).HasMaxLength(200);
            //    client.Property(p => p.Created).IsRequired();

            //    client.HasIndex(p => p.ClientId).IsUnique();

            //    //client.HasMany(p => p.AllowedScopes).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            //    //client.HasMany(p => p.Secrets).WithOne(p => p.Client).HasForeignKey(p => p.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            //    client.HasOne(p => p.Policy).WithOne().HasForeignKey<Client>(p => p.PolicyId);
            //});

            //modelBuilder.Entity<Policy>(policy => {
            //    policy.ToTable(storeOptions.Policy);
            //    policy.HasKey(p => p.Id);

            //    policy.Property(p => p.Created).IsRequired();
            //    //policy.Property(p => p)
            //    //policy.HasOne(p => p.Client).WithMany(p => p.Policies).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
            //    policy.HasMany(p => p.Roles).WithOne(p => p.Policy).HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.Cascade);
            //    policy.HasMany(p => p.Permissions).WithOne(p => p.Policy).HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.Cascade);
            //});

            //modelBuilder.Entity<Role>(role => {
            //    role.ToTable(storeOptions.Role);
            //    role.HasKey(p => p.Id);

            //    role.Property(p => p.Name).HasMaxLength(200).IsRequired();
            //    role.Property(p => p.Created).IsRequired();

            //    role.HasIndex(p => p.Name).IsUnique();

            //    role.HasMany(p => p.Parents).WithOne().OnDelete(DeleteBehavior.Restrict);
            //});

            //modelBuilder.Entity<Permission>(permission => {
            //    permission.ToTable(storeOptions.Permission);
            //    permission.HasKey(p => p.Id);

            //    permission.HasIndex(p => new { p.PolicyId, p.Name }).IsUnique();

            //    permission.Property(p => p.Name).HasMaxLength(200).IsRequired();
            //    permission.Property(p => p.Description).HasMaxLength(200).IsRequired();
            //    permission.Property(p => p.Created).IsRequired();
            //});

            //// ---

            //modelBuilder.Entity<RoleRole>(roleRole => {
            //    roleRole.ToTable(storeOptions.RoleRole);
            //    roleRole.HasKey(p => new { p.RoleId, p.ParentId });

            //    roleRole.HasIndex(p => new { p.RoleId, p.ParentId }).IsUnique();

            //    roleRole.HasOne(p => p.Role).WithMany(p => p.Parents).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Cascade);
            //    roleRole.HasOne(p => p.Parent).WithMany().HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Restrict);
            //});

            //modelBuilder.Entity<RolePermission>(rolePermission => {
            //    rolePermission.ToTable(storeOptions.RolePermission);
            //    rolePermission.HasKey(p => new { p.RoleId, p.PermissionId });

            //    rolePermission.HasIndex(p => new { p.RoleId, p.PermissionId }).IsUnique();

            //    rolePermission.HasOne(p => p.Role).WithMany(p => p.Permissions).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Cascade);
            //    rolePermission.HasOne(p => p.Permission).WithMany().HasForeignKey(p => p.PermissionId).OnDelete(DeleteBehavior.Restrict);
            //});

            //modelBuilder.Entity<RoleIdentity>(roleIdentity => {
            //    roleIdentity.ToTable(storeOptions.RoleIdentity);
            //    roleIdentity.Property(p => p.Identity).HasMaxLength(200).IsRequired();
            //});

            //modelBuilder.Entity<RoleSubject>(roleSubject => {
            //    roleSubject.ToTable(storeOptions.RoleSubject);
            //    roleSubject.Property(p => p.Subject).HasMaxLength(200).IsRequired();
            //});

        }

        private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration) where TEntity : class {
            return String.IsNullOrWhiteSpace(configuration.Schema) ? entityTypeBuilder.ToTable(configuration.Name) : entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
        }

    }
}
