using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PolicyServer1.EntityFramework.Storage.Entities;

namespace PolicyServer1.EntityFramework.Storage.Extensions {
    public static class PermissionExtensions {

        public static IQueryable<Permission> IncludeQuery(this IQueryable<Permission> src) {
            return src
                .Include(p => (p as ScopePermission).Scopes)
                .Include(p => (p as ScopePermission).Resource);
        }

    }
}
