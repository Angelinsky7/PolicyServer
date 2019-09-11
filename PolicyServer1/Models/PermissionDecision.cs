using System;
using System.Collections.Generic;

namespace PolicyServer1.Models {
    public class PermissionDecision {

        public Boolean Result { get; set; }
        public Dictionary<Policy, Boolean> Policies { get; set; } = new Dictionary<Policy, Boolean>();

    }
}
