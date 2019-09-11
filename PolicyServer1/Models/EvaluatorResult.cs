using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PolicyServer1.Models {
    public class EvaluatorResult {

        private readonly Dictionary<Permission, PermissionDecision> _permissionDecisions = new Dictionary<Permission, PermissionDecision>();

        public void AddPermissionResult(Permission permission, PermissionDecision permissionDecision) {
            if (!_permissionDecisions.ContainsKey(permission)) {
                _permissionDecisions.Add(permission, null);
            }
            _permissionDecisions[permission] = permissionDecision;
        }

        public IReadOnlyDictionary<Permission, PermissionDecision> PermissionsDecisions => _permissionDecisions.ToDictionary(p => p.Key, p => p.Value);
    }
}
