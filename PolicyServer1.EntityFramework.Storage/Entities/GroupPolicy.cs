using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class GroupPolicy {

        public Guid Id { get; set; }
        public Policy Base { get; set; }

        public ICollection<GroupPolicyGroup> Users { get; set; } = new HashSet<GroupPolicyGroup>();

    }
}
