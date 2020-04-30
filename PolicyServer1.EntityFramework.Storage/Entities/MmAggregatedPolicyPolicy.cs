using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class MmAggregatedPolicyPolicy {

        public Guid AggregatedPolicyId { get; set; }
        public AggregatedPolicy AggregatedPolicy { get; set; }

        public Guid PolicyId { get; set; }
        public Policy Policy { get; set; }

    }
}
