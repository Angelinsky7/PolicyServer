using System;
using System.Collections.Generic;
using System.Text;
using PolicyServer1.Models;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class AggregatedPolicy : Policy {

        public DecisionStrategy DecisionStrategy { get; set; }

        public ICollection<MmAggregatedPolicyPolicy> Policies { get; set; } = new HashSet<MmAggregatedPolicyPolicy>();

    }
}
