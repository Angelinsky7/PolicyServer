using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class GroupPolicy : Policy {

        public ICollection<GroupPolicyGroup> Groups { get; set; } = new HashSet<GroupPolicyGroup>();

    }
}
