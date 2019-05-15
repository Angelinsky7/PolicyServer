using System;
using System.Collections.Generic;
using System.Linq;

namespace PolicyServer1.Models {
    
    public class Permission {

        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Boolean IsRevoked { get; set; } = false;

    }

    public class PolicyPermission : Permission {

        public Int32 PolicyId { get; set; }
        public String Description { get; set; }

    }

}