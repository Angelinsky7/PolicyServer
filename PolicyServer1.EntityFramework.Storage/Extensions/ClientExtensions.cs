using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PolicyServer1.EntityFramework.Storage.Entities;
using PolicyServer1.EntityFramework.Storage.Interfaces;

namespace PolicyServer1.EntityFramework.Storage {
    public static class ClientExtensions {

        public static async Task LoadDatasAsync(this Client client, IPolicyDbContext context) {
            EntityEntry<Client> entity = context.Entry<Client>(client);

            await entity.Collection(p => p.Secrets)
                .LoadAsync();

            await entity.Collection(p => p.Roles)
                .Query()
                .Include(p => p.Role)
                    .ThenInclude(p => p.Parents)
                        .ThenInclude(p => p.Parent)
                .LoadAsync();

            await entity.Collection(p => p.Scopes)
                .Query()
                .Include(p => p.Scope)
                .LoadAsync();

            await entity.Collection(p => p.Resources)
                .Query()
                .Include(p => p.Resource)
                    .ThenInclude(p => p.Scopes)
                        .ThenInclude(p => p.Scope)
                .LoadAsync();

            await entity.Collection(p => p.Policies)
                .Query()
                .Include(p => p.Policy)
                    .ThenInclude(p => (p as RolePolicy).Roles)
                        .ThenInclude(p => p.Role)
                .Include(p => p.Policy)
                        .ThenInclude(p => (p as UserPolicy).Users)
                .Include(p => p.Policy)
                        .ThenInclude(p => (p as GroupPolicy).Groups)
                .Include(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).DayOfMonth)
                .Include(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).Hour)
                .Include(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).Minute)
                .Include(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).Month)
                .Include(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).Year)
                .Include(p => p.Policy)
                        .ThenInclude(p => (p as ClientPolicy).Clients)
                .Include(p => p.Policy)
                        .ThenInclude(p => (p as AggregatedPolicy).Policies)
                            .ThenInclude(p => p.Policy)
                .LoadAsync();

            await entity.Collection(p => p.Permissions)
                .Query()
                .Include(p => p.Permission)
                        .ThenInclude(p => p.Policies)
                            .ThenInclude(p => p.Policy)
                .Include(p => p.Permission)
                        .ThenInclude(p => (p as ScopePermission).Scopes)
                            .ThenInclude(p => p.Scope)
                .Include(p => p.Permission)
                        .ThenInclude(p => (p as ScopePermission).Resource)
                .Include(p => p.Permission)
                        .ThenInclude(p => (p as ResourcePermission).Resource)
                            .ThenInclude(p => p.Scopes)
                                .ThenInclude(p => p.Scope)
                .LoadAsync();
        }

        public static IQueryable<Client> IncludeQuery(this IQueryable<Client> src) {
            return src
                .Include(p => p.Secrets)

                .Include(p => p.Scopes)
                    .ThenInclude(p => p.Scope)

                .IncludeResources()
                .IncludeRoles()
                .IncludePolicies()
                .IncludePermissions();
        }

        public static IQueryable<Client> IncludeResources(this IQueryable<Client> src) {
            return src.Include(p => p.Resources)
                .ThenInclude(p => p.Resource)
                    .ThenInclude(p => p.Scopes)
                        .ThenInclude(p => p.Scope);
        }

        public static IQueryable<Client> IncludeRoles(this IQueryable<Client> src) {
            return src.Include(p => p.Roles)
                    .ThenInclude(p => p.Role)
                        .ThenInclude(p => p.Parents)
                            .ThenInclude(p => p.Parent);
        }

        public static IQueryable<Client> IncludePolicies(this IQueryable<Client> src) {
            return src.Include(p => p.Policies)
                .ThenInclude(p => p.Policy)
                    .ThenInclude(p => (p as RolePolicy).Roles)
                        .ThenInclude(p => p.Role)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                        .ThenInclude(p => (p as UserPolicy).Users)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                        .ThenInclude(p => (p as GroupPolicy).Groups)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).DayOfMonth)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).Hour)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).Minute)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).Month)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                        .ThenInclude(p => (p as TimePolicy).Year)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                        .ThenInclude(p => (p as ClientPolicy).Clients)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy)
                        .ThenInclude(p => (p as AggregatedPolicy).Policies)
                            .ThenInclude(p => p.Policy);

        }

        public static IQueryable<Client> IncludePermissions(this IQueryable<Client> src) {
            return src.Include(p => p.Permissions)
                    .ThenInclude(p => p.Permission)
                        .ThenInclude(p => p.Policies)
                            .ThenInclude(p => p.Policy)
                .Include(p => p.Permissions)
                    .ThenInclude(p => p.Permission)
                        .ThenInclude(p => (p as ScopePermission).Scopes)
                            .ThenInclude(p => p.Scope)
                .Include(p => p.Permissions)
                    .ThenInclude(p => p.Permission)
                        .ThenInclude(p => (p as ScopePermission).Resource)
                .Include(p => p.Permissions)
                    .ThenInclude(p => p.Permission)
                        .ThenInclude(p => (p as ResourcePermission).Resource)
                            .ThenInclude(p => p.Scopes)
                                .ThenInclude(p => p.Scope);
        }

    }
}
