using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class ClientOption {

        public PolicyEnforcement PolicyEnforcement { get; set; }
        public DecisionStrategy DecisionStrategy { get; set; }

    }
}
