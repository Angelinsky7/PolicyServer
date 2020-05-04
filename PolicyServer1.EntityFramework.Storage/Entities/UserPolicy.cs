using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class UserPolicy : Policy {

        public ICollection<UserPolicyUser> Users { get; set; } = new HashSet<UserPolicyUser>();

    }
}
