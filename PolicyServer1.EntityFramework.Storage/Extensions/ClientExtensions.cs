using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PolicyServer1.EntityFramework.Storage.Entities;

namespace PolicyServer1.EntityFramework.Storage {
    public static class ClientExtensions {

        public static IQueryable<Client> IncludeQuery(this IQueryable<Client> src) {
            return src
                .Include(p => p.Secrets)

                .Include(p => p.Scopes)
                    .ThenInclude(p => p.Scope)

                .Include(p => p.Resources)
                    .ThenInclude(p => p.Resource)
                        .ThenInclude(p => p.Scopes)
                            .ThenInclude(p => p.Scope)

                .IncludeRoles()
                .IncludePolicies()
                .IncludePermissions();
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
                            .ThenInclude(p => p.Parents)
                                .ThenInclude(p => p.Parent)
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
