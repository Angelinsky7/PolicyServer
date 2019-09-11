using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.Models;

namespace PolicyServer1.Models {
    public static class PermissionExtensions {

        public static async Task EvaluateAsync(this Permission permission, EvaluatorRequest request) {
            Nullable<Boolean> permissionDecision = null;
            Dictionary<Policy, Boolean> policyResult = new Dictionary<Policy, Boolean>();

            Int32 permissionPass = 0;
            Int32 permissionRefuse = 0;

            foreach (Policy policy in permission.Policies) {
                if (!request.Cache.HasPolicyCache(policy)) {
                    request.Cache.AddPolicyCache(policy, await policy.EvaluateAsync(request));
                }
                Boolean evaluator = request.Cache.GetPolicyCacheWithoutNullable(policy);
                policyResult.Add(policy, evaluator);

                if (evaluator) {
                    ++permissionPass;
                } else {
                    ++permissionRefuse;
                }
            }

            switch (permission.DecisionStrategy) {
                case DecisionStrategy.Affirmative:
                    permissionDecision = permissionPass > 0;
                    break;
                case DecisionStrategy.Consensus:
                    permissionDecision = (permissionPass - permissionRefuse) > 0;
                    break;
                case DecisionStrategy.Unanimous:
                    permissionDecision = permissionRefuse == 0;
                    break;
            }

            request.Results.AddPermissionResult(permission, new PermissionDecision {
                Result = permissionDecision.Value,
                Policies = policyResult
            });

        }

    }

}
