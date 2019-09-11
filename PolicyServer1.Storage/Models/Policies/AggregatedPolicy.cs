using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Models {
    public class AggregatedPolicy : Policy {

        public ICollection<Policy> Policies { get; set; } = new HashSet<Policy>();

        public DecisionStrategy DecisionStrategy { get; set; }

        public override async Task<Boolean> EvaluateAsync(IEvaluatorRequest request) {
            Nullable<Boolean> result = false;

            Int32 resultGranted = 0;
            Int32 resultDenied = 0;

            foreach (Policy policy in Policies) {
                Boolean nextResult = await policy.EvaluateAsync(request);
                if (nextResult) {
                    ++resultGranted;
                } else {
                    ++resultDenied;
                }
            }

            switch (DecisionStrategy) {
                case DecisionStrategy.Affirmative:
                    result = resultGranted > 0;
                    break;
                case DecisionStrategy.Consensus:
                    result = resultGranted - resultDenied > 0;
                    break;
                case DecisionStrategy.Unanimous:
                    result = resultDenied == 0;
                    break;

            }

            return result.Value;

        }

    }
}
