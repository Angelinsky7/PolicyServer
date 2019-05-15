using System;
using System.Collections.Generic;

namespace PolicyServer1.Models {

    public class PolicyResult {

        public String Issuer { get; set;}
        public String PolicyHash { get; set; }
        public DateTime LastPolicyChangeDate { get; set; }
        public IEnumerable<String> Roles { get; set; }
        public IEnumerable<String> Permissions { get; set; }
    }
}
