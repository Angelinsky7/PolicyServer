using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PolicyServer1.Models {
    public class EvaluatorRequest : IEvaluatorRequest {

        public Client Client { get; set; }
        public ClaimsPrincipal User { get; set; }
        public IEnumerable<String> Permissions { get; set; }

        public EvaluatorResult Result { get; set; } = new EvaluatorResult();
        public EvaluatorCache Cache { get; set; } = new EvaluatorCache();

    }
}
