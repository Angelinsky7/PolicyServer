using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class ClientOption {

        public PolicyEnforcement PolicyEnforcement { get; set; } = PolicyEnforcement.Enforcing;
        public DecisionStrategy DecisionStrategy { get; set; } = DecisionStrategy.Affirmative;

        public Boolean AnalyseModeEnabled { get; set; } = false;
        public String PermissionSplitter { get; set; } = "#";
    }
}
