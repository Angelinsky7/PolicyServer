using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class UserPolicyUser {

        public Int64 Id { get; set; }

        public Guid UserPolicyId { get; set; }
        public UserPolicy UserPolicy { get; set; }

        public String User { get; set; }

    }
}
