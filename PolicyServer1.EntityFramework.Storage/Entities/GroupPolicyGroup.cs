using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class GroupPolicyGroup {

        public Int64 Id { get; set; }

        public Guid GroupPolicyId { get; set; }
        public GroupPolicy GroupPolicy { get; set; }

        public String Group { get; set; }

    }
}
