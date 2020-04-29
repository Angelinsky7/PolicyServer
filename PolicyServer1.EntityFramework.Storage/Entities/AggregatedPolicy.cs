using System;
using System.Collections.Generic;
using System.Text;
using PolicyServer1.Models;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class AggregatedPolicy {

        public Guid Id { get; set; }
        public Policy Base { get; set; }

        public DecisionStrategy DecisionStrategy { get; set; }

        public ICollection<AggregatedPolicyPolicy> Policies { get; set; } = new HashSet<AggregatedPolicyPolicy>();

    }
}
