using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace PolicyServer1.Models {
    public class GroupPolicy : Policy {

        public ICollection<String> Groups { get; set; } = new HashSet<String>();

        public override Task<Boolean> EvaluateAsync(IEvaluatorRequest request) {
            //TODO(demarco): this is not correct !!!
            IEnumerable<String> groupes = request.User.FindAll("role").Select(p => p.Value);

            request.Result = Groups.Any(p => groupes.Any(a => a == p));
            return base.EvaluateAsync(request);
        }
    }
}
