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
                    .ThenInclude(p => p.Secret)
                .Include(p => p.Scopes)
                    .ThenInclude(p => p.Scope)
                .Include(p => p.Resources)
                    .ThenInclude(p => p.Resource)
                        .ThenInclude(p => p.Scopes)
                            .ThenInclude(p => p.Scope)
                .Include(p => p.Roles)
                    .ThenInclude(p => p.Role)
                        .ThenInclude(p => p.Parents)
                            .ThenInclude(p => p.Parent)
                .Include(p => p.Permissions)
                    .ThenInclude(p => p.Permission)
                        .ThenInclude(p => p.Policies)
                            .ThenInclude(p => p.Policy)
                .Include(p => p.Policies)
                    .ThenInclude(p => p.Policy);
        }

    }
}
