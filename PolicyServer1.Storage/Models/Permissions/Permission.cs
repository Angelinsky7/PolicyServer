using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Models {
    
    public abstract class Permission {

        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public String Description { get; set; }

        public DecisionStrategy DecisionStrategy { get; set; }

        public ICollection<Policy> Policies { get; set; } = new HashSet<Policy>();

        //public abstract Task<Object> EvaluateAsync(ClaimsPrincipal user);

    }

}