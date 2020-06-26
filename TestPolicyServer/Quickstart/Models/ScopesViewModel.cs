using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolicyServer1.Models;

namespace TestPolicyServer.Quickstart {
    public class ScopesViewModel : ListViewModel {
        public IEnumerable<Scope> Scopes { get; set; }
    }

}
