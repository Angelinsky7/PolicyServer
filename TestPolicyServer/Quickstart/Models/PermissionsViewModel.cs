using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolicyServer1.Models;

namespace TestPolicyServer.Quickstart {
    public class PermissionsViewModel : ListViewModel {
        public IEnumerable<Permission> Permissions { get; set; }
    }

}
