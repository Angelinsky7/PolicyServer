using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class EvaluationAnalyse {
        public ICollection<EvaluationAnalyseItem> Items { get; set; } = new HashSet<EvaluationAnalyseItem>();
    }

    public class EvaluationAnalyseItem {
        public Resource Resource { get; set; }
        public Boolean Granted { get; set; }
        public ICollection<Scope> Scopes { get; set; } = new HashSet<Scope>();
        public ICollection<EvaluationAnalysePermissionItem> Permissions { get; set; } = new HashSet<EvaluationAnalysePermissionItem>();
    }

    public class EvaluationAnalysePermissionItem {
        public Permission Permission { get; set; }
        public Boolean Granted { get; set; }
        public DecisionStrategy Strategy { get; set; }
        public ICollection<Scope> Scopes { get; set; } = new HashSet<Scope>();
        public ICollection<EvaluationAnalysePolicyItem> Policies { get; set; } = new HashSet<EvaluationAnalysePolicyItem>();
    }

    public class EvaluationAnalysePolicyItem {
        public Policy Policy { get; set; }
        public Boolean Granted { get; set; }
    }

}
