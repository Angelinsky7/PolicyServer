using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Models {

    public class Policy {

        public Int32 Id { get; set; }
        public String Hash { get; set; }
        public DateTime LastPolicyChangeDate { get; set; }

        public List<Role> Roles { get; internal set; } = new List<Role>();
        public List<Permission> Permissions { get; internal set; } = new List<Permission>();
    }
}