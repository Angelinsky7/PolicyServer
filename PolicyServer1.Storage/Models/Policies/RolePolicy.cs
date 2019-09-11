using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Models {
    public class RolePolicy : Policy {

        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        public override Task<Boolean> EvaluateAsync(IEvaluatorRequest request) {
            //TODO(demarco): this is not correct !!!
            IEnumerable<String> check = request.User.FindAll("role").Select(p => p.Value);

            return Task.FromResult(CheckRecursiveRole(Roles, check));
        }

        private Boolean CheckRecursiveRole(IEnumerable<Role> roles, IEnumerable<String> check) {
            Boolean test = roles.Select(p => p.Name).Any(p => check.Any(a => a == p));
            if (test) { return true; }

            foreach (Role role in roles) {
                if (role.Parents != null && role.Parents.Count != 0) {
                    if (CheckRecursiveRole(role.Parents, check)) {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
