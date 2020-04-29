using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class UserPolicy {

        public Guid Id { get; set; }
        public Policy Base { get; set; }

        public ICollection<UserPolicyUser> Users { get; set; } = new HashSet<UserPolicyUser>();

    }
}
