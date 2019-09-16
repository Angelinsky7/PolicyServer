using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PolicyServer1.Models {
    public class EvaluatorRequest : IEvaluatorRequest {

        public Client Client { get; set; }
        public ClaimsPrincipal User { get; set; }
        public IEnumerable<String> Permissions { get; set; }

        public Boolean Result { get; set; }

        public EvaluatorResult EvaluatorResults { get; set; } = new EvaluatorResult();
        public EvaluatorCache Cache { get; set; } = new EvaluatorCache();

        public ICollection<ResouceScopeResult> ResourceScopeResults { get; set; } = new HashSet<ResouceScopeResult>();

    }
}
