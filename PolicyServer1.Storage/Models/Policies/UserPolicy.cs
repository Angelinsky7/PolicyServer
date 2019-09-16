using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Models {
    public class UserPolicy : Policy {

        public ICollection<String> Users { get; set; } = new HashSet<String>();

        public override Task<Boolean> EvaluateAsync(IEvaluatorRequest request) {
            //TODO(demarco): this is not correct

            request.Result = Users.Contains(request.User.FindFirst("sub")?.Value);
            return base.EvaluateAsync(request);
        }
    }
}
