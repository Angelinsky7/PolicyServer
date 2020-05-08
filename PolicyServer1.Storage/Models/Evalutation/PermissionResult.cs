using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models.Evalutation {
    public class PermissionResult {

        public String ClientId { get; set; }
        public String Issuer { get; set; }
        public Int32 ExpireIn { get; set; }
        public IEnumerable<ResourceResult> Results { get; set; } = new HashSet<ResourceResult>();

    }
}
