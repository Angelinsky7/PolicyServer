using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolicyServer1.Models;
using TestPolicyServer.Extensions;

namespace TestPolicyServer.Quickstart {
    public class PermissionsViewModel : ListViewModel {
        public IEnumerable<Permission> Permissions { get; set; }
    }

    [Obsolete]
    public class PermissionsFormResourceViewModel {
        public ResourcePermission Item { get; set; }
        public IEnumerable<Resource> Resources { get; set; }
        public IEnumerable<DecisionStrategy> DecisionStrategies { get; set; } = EnumExtensions.GetAsList<DecisionStrategy>();
    }

}
