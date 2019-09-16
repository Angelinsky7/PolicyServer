using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class EvaluationAnalyse {
        public ICollection<EvaluationAnalyseItem> Items { get; set; } = new HashSet<EvaluationAnalyseItem>();
    }

    public class EvaluationAnalyseItem {
        public Guid ResourceId { get; set; }
        public String ResourceName { get; set; }
        public Boolean Granted { get; set; }
        public String Strategy { get; set; }
        public ICollection<String> Scopes { get; set; } = new HashSet<String>();
        public ICollection<EvaluationAnalysePermissionItem> Permissions { get; set; } = new HashSet<EvaluationAnalysePermissionItem>();
    }

    public class EvaluationAnalysePermissionItem {
        public Guid PermissionId { get; set; }
        public String PermissionName { get; set; }
        public Boolean Granted { get; set; }
        public String Strategy { get; set; }
        public ICollection<String> Scopes { get; set; } = new HashSet<String>();
        public ICollection<EvaluationAnalysePolicyItem> Policies { get; set; } = new HashSet<EvaluationAnalysePolicyItem>();
    }

    public class EvaluationAnalysePolicyItem {
        public Guid PolicyId { get; set; }
        public String PolicyName { get; set; }
        public Boolean Granted { get; set; }
    }

}
