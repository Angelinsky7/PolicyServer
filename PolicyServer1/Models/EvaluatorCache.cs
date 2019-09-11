using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class EvaluatorCache {

        private readonly Dictionary<Policy, Nullable<Boolean>> _policyResult = new Dictionary<Policy, Nullable<Boolean>>();

        public void AddPolicyCache(Policy policy, Boolean decision) {
            if (!_policyResult.ContainsKey(policy)) {
                _policyResult.Add(policy, null);
            }
            _policyResult[policy] = decision;
        }

        public Boolean HasPolicyCache(Policy policy) {
            return _policyResult.ContainsKey(policy);
        }

        public Nullable<Boolean> GetPolicyCache(Policy policy) {
            if (HasPolicyCache(policy)) {
                return _policyResult[policy];
            }
            return null;
        }

        public Boolean GetPolicyCacheWithoutNullable(Policy policy) {
            if (HasPolicyCache(policy)) {
                return _policyResult[policy].Value;
            }
            throw new InvalidOperationException();
        }

    }
}
