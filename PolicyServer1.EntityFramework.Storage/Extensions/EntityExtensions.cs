using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PolicyServer1.EntityFramework.Storage.Entities;

namespace PolicyServer1.EntityFramework.Storage {
    public static class EntityExtensions {

        public static void PrepareBeforeInsert(this Client client) {
            foreach (var resource in client.Resources.Select(p => p.Resource)) { resource.PrepareBeforeInsert(); }
            foreach (var policy in client.Policies.Select(p => p.Policy)) { policy.PrepareBeforeInsert(); }
            foreach (var scope in client.Scopes.Select(p => p.Scope)) { scope.PrepareBeforeInsert(); }
            foreach (var role in client.Roles.Select(p => p.Role)) { role.PrepareBeforeInsert(); }
            foreach (var permission in client.Permissions.Select(p => p.Permission)) { permission.PrepareBeforeInsert(); }
        }

        public static void PrepareBeforeInsert(this Resource resource) {
            foreach (MmResourceScope scope in resource.Scopes) {
                scope.PrepareBeforeInsert();
            }
        }
        public static void PrepareBeforeInsert(this Policy policy) { }
        public static void PrepareBeforeInsert(this Scope scope) { }
        public static void PrepareBeforeInsert(this Role role) {
            foreach (var parent in role.Parents) {
                parent.PrepareBeforeInsert();
            }
        }
        public static void PrepareBeforeInsert(this Permission permission) {
            foreach (var policy in permission.Policies) {
                policy.PrepareBeforeInsert();
            }
        }

        public static void PrepareBeforeInsert(this MmResourceScope scope) {
            scope.Resource = null;
            scope.Scope = null;
        }
        public static void PrepareBeforeInsert(this MmRoleRole role) {
            role.Role = null;
            role.Parent = null;
        }
        public static void PrepareBeforeInsert(this MmPermissionPolicy permission) {
            permission.Permission = null;
            permission.Policy = null;
        }

    }
}
