using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

#pragma warning disable IDE1006 // Naming Styles
        internal Dictionary<Policy, Nullable<Boolean>> _AnalyseCache => _policyResult.ToDictionary(p => p.Key, p => p.Value);
#pragma warning restore IDE1006 // Naming Styles

    }
}
