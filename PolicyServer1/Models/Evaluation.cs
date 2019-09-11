using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class Evaluation {
        public ICollection<EvaluationItem> Results { get; set; } = new HashSet<EvaluationItem>();
    }
}
