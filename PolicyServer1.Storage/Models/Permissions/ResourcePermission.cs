using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Models {
    public class ResourcePermission : Permission {

        public String ResouceType { get; set; }
        public Resource Resource { get; set; }

        //public override Task<Object> EvaluateAsync(ClaimsPrincipal user) {
        //    return Task.FromResult((Object)Resource);
        //}

    }
}
