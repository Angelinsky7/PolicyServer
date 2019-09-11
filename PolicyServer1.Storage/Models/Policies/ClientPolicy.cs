using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PolicyServer1.Models {
    public class ClientPolicy : Policy {

        public ICollection<String> Clients { get; set; } = new HashSet<String>();

        public override Task<Boolean> EvaluateAsync(IEvaluatorRequest request) {
            //TODO(demarco): this is not correct

            return Task.FromResult(Clients.Contains(request.User.FindFirst("client_id")?.Value));
        }
    }
}
