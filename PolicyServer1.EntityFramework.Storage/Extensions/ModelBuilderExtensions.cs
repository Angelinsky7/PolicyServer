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

                scope.HasIndex(p => p.Name).IsUnique(true);

                scope.Property(p => p.Id).ValueGeneratedNever();
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

                role.HasIndex(p => p.Name).IsUnique(true);

                role.Property(p => p.Id).ValueGeneratedNever();
                role.Property(p => p.Name).HasMaxLength(200).IsRequired();
                role.Property(p => p.Description).HasMaxLength(200);

                role.HasMany(p => p.Parents).WithOne(p => p.Role).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.ClientCascade);

                role.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                role.Property(p => p.Updated).ValueGeneratedOnUpdate();
                role.Property(p => p.LastAccessed);
            });

            modelBuilder.Entity<MmRoleRole>(roleRole => {
                roleRole.ToTable(storeOptions.MmRoleRole);
                roleRole.HasKey(p => new { p.RoleId, p.ParentId });

                roleRole.HasIndex(p => new { p.RoleId, p.ParentId }).IsUnique();

                roleRole.HasOne(p => p.Role).WithMany(p => p.Parents).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Cascade);
                roleRole.HasOne(p => p.Parent).WithMany().HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<Policy>(policy => {
                policy.ToTable(storeOptions.Policy);
                policy.HasKey(p => p.Id);

                policy.Property(p => p.Id).ValueGeneratedNever();
                policy.Property(p => p.Name).HasMaxLength(200).IsRequired();
                policy.Property(p => p.Description).HasMaxLength(200);
                policy.Property(p => p.Logic).IsRequired();

                policy.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                policy.Property(p => p.Updated).ValueGeneratedOnUpdate();
                policy.Property(p => p.LastAccessed);

                policy.HasDiscriminator<String>("PolicyType")
                    .HasValue<AggregatedPolicy>(nameof(AggregatedPolicy))
                    .HasValue<ClientPolicy>(nameof(ClientPolicy))
                    .HasValue<GroupPolicy>(nameof(GroupPolicy))
                    .HasValue<RolePolicy>(nameof(RolePolicy))
                    .HasValue<TimePolicy>(nameof(TimePolicy))
                    .HasValue<UserPolicy>(nameof(UserPolicy));

                policy.HasIndex(nameof(Policy.Name), "PolicyType").IsUnique(true);

            });

            modelBuilder.Entity<RolePolicy>(rolePolicy => {
                rolePolicy.HasMany(p => p.Roles).WithOne(p => p.RolePolicy).HasForeignKey(p => p.RolePolicyId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<MmRolePolicyRole>(rolePolicyRole => {
                rolePolicyRole.ToTable(storeOptions.MmRolePolicyRole);
                rolePolicyRole.HasKey(p => new { p.RolePolicyId, p.RoleId });

                rolePolicyRole.HasIndex(p => new { p.RolePolicyId, p.RoleId }).IsUnique();

                rolePolicyRole.HasOne(p => p.RolePolicy).WithMany(p => p.Roles).HasForeignKey(p => p.RolePolicyId).OnDelete(DeleteBehavior.Cascade);
                rolePolicyRole.HasOne(p => p.Role).WithMany().HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<TimePolicy>(timePolicy => {
                timePolicy.HasOne(p => p.DayOfMonth).WithOne().HasForeignKey<TimePolicy>(p => p.DayOfMonthId).OnDelete(DeleteBehavior.Restrict);
                timePolicy.HasOne(p => p.Month).WithOne().HasForeignKey<TimePolicy>(p => p.MonthId).OnDelete(DeleteBehavior.Restrict);
                timePolicy.HasOne(p => p.Year).WithOne().HasForeignKey<TimePolicy>(p => p.YearId).OnDelete(DeleteBehavior.Restrict);
                timePolicy.HasOne(p => p.Hour).WithOne().HasForeignKey<TimePolicy>(p => p.HourId).OnDelete(DeleteBehavior.Restrict);
                timePolicy.HasOne(p => p.Minute).WithOne().HasForeignKey<TimePolicy>(p => p.MinuteId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserPolicy>(userPolicy => {
                userPolicy.HasMany(p => p.Users).WithOne(p => p.UserPolicy).HasForeignKey(p => p.UserPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserPolicyUser>(userPolicyUser => {
                userPolicyUser.ToTable(storeOptions.UserPolicyUser);
                userPolicyUser.HasKey(p => p.Id);

                userPolicyUser.Property(p => p.User).HasMaxLength(200).IsRequired();

                userPolicyUser.HasOne(p => p.UserPolicy).WithMany(p => p.Users).HasForeignKey(p => p.UserPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GroupPolicy>(groupPolicy => {
                groupPolicy.HasMany(p => p.Groups).WithOne(p => p.GroupPolicy).HasForeignKey(p => p.GroupPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GroupPolicyGroup>(groupPolicyGroup => {
                groupPolicyGroup.ToTable(storeOptions.GroupPolicyGroup);
                groupPolicyGroup.HasKey(p => p.Id);

                groupPolicyGroup.HasOne(p => p.GroupPolicy).WithMany(p => p.Groups).HasForeignKey(p => p.GroupPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ClientPolicy>((Action<EntityTypeBuilder<ClientPolicy>>)(clientPolicy => {
                clientPolicy.HasMany(p => p.Clients).WithOne(p => p.ClientPolicy).HasForeignKey(p => p.ClientPolicyId).OnDelete(DeleteBehavior.Cascade);
            }));

            modelBuilder.Entity<ClientPolicyClient>(clientPolicyClient => {
                clientPolicyClient.ToTable(storeOptions.ClientPolicyClient);
                clientPolicyClient.HasKey(p => p.Id);

                clientPolicyClient.HasOne(p => p.ClientPolicy).WithMany(p => p.Clients).HasForeignKey(p => p.ClientPolicyId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AggregatedPolicy>(aggregatedPolicy => {
                aggregatedPolicy.HasMany(p => p.Policies).WithOne(p => p.AggregatedPolicy).HasForeignKey(p => p.AggregatedPolicyId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<MmAggregatedPolicyPolicy>(aggregatedPolicyPolicy => {
                aggregatedPolicyPolicy.ToTable(storeOptions.MmAggregatedPolicyPolicy);
                aggregatedPolicyPolicy.HasKey(p => new { p.AggregatedPolicyId, p.PolicyId });

                aggregatedPolicyPolicy.HasIndex(p => new { p.AggregatedPolicyId, p.PolicyId }).IsUnique();

                aggregatedPolicyPolicy.HasOne(p => p.AggregatedPolicy).WithMany(p => p.Policies).HasForeignKey(p => p.AggregatedPolicyId).OnDelete(DeleteBehavior.Cascade);
                aggregatedPolicyPolicy.HasOne(p => p.Policy).WithMany().HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<Resource>(resource => {
                resource.ToTable(storeOptions.Resource);
                resource.HasKey(p => p.Id);

                resource.HasIndex(p => p.Name).IsUnique(true);

                resource.Property(p => p.Id).ValueGeneratedNever();
                resource.Property(p => p.Name).HasMaxLength(200).IsRequired();
                resource.Property(p => p.DisplayName).HasMaxLength(200).IsRequired();
                resource.Property(p => p.Type).HasMaxLength(200);
                resource.Property(p => p.IconUri).HasMaxLength(2000);

                resource.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                resource.Property(p => p.Updated).ValueGeneratedOnUpdate();
                resource.Property(p => p.LastAccessed);

                resource.HasMany(p => p.Uris).WithOne(p => p.Resource).HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.Cascade);
                resource.HasMany(p => p.Scopes).WithOne(p => p.Resource).HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ResourceUri>(resourceUri => {
                resourceUri.ToTable(storeOptions.ResourceUri);
                resourceUri.HasKey(p => p.Id);

                resourceUri.HasOne(p => p.Resource).WithMany(p => p.Uris).HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MmResourceScope>(resourceScope => {
                resourceScope.ToTable(storeOptions.MmResourceScope);
                resourceScope.HasKey(p => new { p.ResourceId, p.ScopeId });

                resourceScope.HasIndex(p => new { p.ResourceId, p.ScopeId }).IsUnique();

                resourceScope.HasOne(p => p.Resource).WithMany(p => p.Scopes).HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.Cascade);
                resourceScope.HasOne(p => p.Scope).WithMany().HasForeignKey(p => p.ScopeId).OnDelete(DeleteBehavior.ClientCascade);
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

                secret.HasDiscriminator<String>("SecretType")
                    .HasValue<ClientSecret>(nameof(ClientSecret));
            });

            modelBuilder.Entity<Permission>(permission => {
                permission.ToTable(storeOptions.Permission);
                permission.HasKey(p => p.Id);

                permission.HasIndex(p => p.Name).IsUnique(true);

                permission.Property(p => p.Id).ValueGeneratedNever();
                permission.Property(p => p.Name).HasMaxLength(200).IsRequired();
                permission.Property(p => p.Description).HasMaxLength(200);
                permission.Property(p => p.DecisionStrategy).IsRequired();

                permission.Property(p => p.Created).ValueGeneratedOnAdd().IsRequired();
                permission.Property(p => p.Updated).ValueGeneratedOnUpdate();
                permission.Property(p => p.LastAccessed);

                permission.HasMany(p => p.Policies).WithOne(p => p.Permission).HasForeignKey(p => p.PermissionId).OnDelete(DeleteBehavior.Cascade);

                permission.HasDiscriminator<String>("PermissionType")
                    .HasValue<ScopePermission>(nameof(ScopePermission))
                    .HasValue<ResourcePermission>(nameof(ResourcePermission));
            });

            modelBuilder.Entity<MmPermissionPolicy>(permissionPolicy => {
                permissionPolicy.ToTable(storeOptions.MmPermissionPolicy);
                permissionPolicy.HasKey(p => new { p.PermissionId, p.PolicyId });

                permissionPolicy.HasIndex(p => new { p.PermissionId, p.PolicyId }).IsUnique();

                permissionPolicy.HasOne(p => p.Permission).WithMany(p => p.Policies).HasForeignKey(p => p.PermissionId).OnDelete(DeleteBehavior.Cascade);
                permissionPolicy.HasOne(p => p.Policy).WithMany().HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<ScopePermission>(scopePermission => {
                scopePermission.HasOne(p => p.Resource).WithMany().HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.ClientCascade);
                scopePermission.HasMany(p => p.Scopes).WithOne(p => p.ScopePermission).HasForeignKey(p => p.ScopePermissionId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MmScopePermissionScope>(scopePermissionScope => {
                scopePermissionScope.ToTable(storeOptions.MmScopePermissionScope);
                scopePermissionScope.HasKey(p => new { p.ScopePermissionId, p.ScopeId });

                scopePermissionScope.HasIndex(p => new { p.ScopePermissionId, p.ScopeId }).IsUnique();

                scopePermissionScope.HasOne(p => p.ScopePermission).WithMany(p => p.Scopes).HasForeignKey(p => p.ScopePermissionId).OnDelete(DeleteBehavior.Cascade);
                scopePermissionScope.HasOne(p => p.Scope).WithMany().HasForeignKey(p => p.ScopeId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<ResourcePermission>(resourcePermission => {
                resourcePermission.Property(p => p.ResouceType).HasMaxLength(200);

                resourcePermission.HasOne(p => p.Resource).WithMany().HasForeignKey(p => p.ResourceId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MmScopePermissionScope>(scopePermissionScope => {
                scopePermissionScope.ToTable(storeOptions.MmScopePermissionScope);
                scopePermissionScope.HasKey(p => new { p.ScopePermissionId, p.ScopeId });

                scopePermissionScope.HasIndex(p => new { p.ScopePermissionId, p.ScopeId }).IsUnique();

                scopePermissionScope.HasOne(p => p.ScopePermission).WithMany(p => p.Scopes).HasForeignKey(p => p.ScopePermissionId).OnDelete(DeleteBehavior.Cascade);
                scopePermissionScope.HasOne(p => p.Scope).WithMany().HasForeignKey(p => p.ScopeId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<Client>(client => {
                client.ToTable(storeOptions.Client);
                client.HasKey(p => p.Id);

                client.HasIndex(p => p.ClientId).IsUnique();

                client.Property(p => p.Id).ValueGeneratedNever();
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
                clientSecret.HasOne(p => p.Client).WithMany(p => p.Secrets).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                //clientSecret.HasOne(p => p.Secret).WithMany().HasForeignKey(p => p.SecretId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<MmClientResource>(clientResource => {
                clientResource.ToTable(storeOptions.MmClientResource);
                clientResource.HasKey(p => new { p.ClientId, p.ResourceId });

                clientResource.HasIndex(p => new { p.ClientId, p.ResourceId }).IsUnique();

                clientResource.HasOne(p => p.Client).WithMany(p => p.Resources).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientResource.HasOne(p => p.Resource).WithMany().HasForeignKey(p => p.ResourceId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<MmClientScope>(clientScope => {
                clientScope.ToTable(storeOptions.MmClientScope);
                clientScope.HasKey(p => new { p.ClientId, p.ScopeId });

                clientScope.HasIndex(p => new { p.ClientId, p.ScopeId }).IsUnique();

                clientScope.HasOne(p => p.Client).WithMany(p => p.Scopes).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientScope.HasOne(p => p.Scope).WithMany().HasForeignKey(p => p.ScopeId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<MmClientRole>(clientRole => {
                clientRole.ToTable(storeOptions.MmClientRole);
                clientRole.HasKey(p => new { p.ClientId, p.RoleId });

                clientRole.HasIndex(p => new { p.ClientId, p.RoleId }).IsUnique();

                clientRole.HasOne(p => p.Client).WithMany(p => p.Roles).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientRole.HasOne(p => p.Role).WithMany().HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<MmClientPolicy>((Action<EntityTypeBuilder<MmClientPolicy>>)(clientPolicy => {
                clientPolicy.ToTable((TableConfiguration)storeOptions.MmClientPolicy);
                clientPolicy.HasKey(p => (new { p.ClientId, p.PolicyId }));

                clientPolicy.HasIndex(p => (new { p.ClientId, p.PolicyId })).IsUnique();

                clientPolicy.HasOne(p => p.Client).WithMany(p => p.Policies).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientPolicy.HasOne(p => p.Policy).WithMany().HasForeignKey(p => p.PolicyId).OnDelete(DeleteBehavior.ClientCascade);
            }));

            modelBuilder.Entity<MmClientPermission>(clientPermission => {
                clientPermission.ToTable(storeOptions.MmClientPermission);
                clientPermission.HasKey(p => new { p.ClientId, p.PermissionId });

                clientPermission.HasIndex(p => new { p.ClientId, p.PermissionId }).IsUnique();

                clientPermission.HasOne(p => p.Client).WithMany(p => p.Permissions).HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.Cascade);
                clientPermission.HasOne(p => p.Permission).WithMany().HasForeignKey(p => p.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
            });

        }

        private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration) where TEntity : class {
            return String.IsNullOrWhiteSpace(configuration.Schema) ? entityTypeBuilder.ToTable(configuration.Name) : entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
        }

    }
}
