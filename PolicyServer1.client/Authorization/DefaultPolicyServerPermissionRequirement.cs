using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace PolicyServer1.Authorization {
    public class DefaultPolicyServerPermissionRequirement : IAuthorizationRequirement {
        public String Name { get; private set; }

        public DefaultPolicyServerPermissionRequirement(String name) {
            Name = name;
        }
                
    }
}
