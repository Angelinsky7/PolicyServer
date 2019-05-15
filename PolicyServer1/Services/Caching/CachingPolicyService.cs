using Microsoft.Extensions.Logging;
using PolicyServer1.Configuration;
using PolicyServer1.Extensions;
using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Services.Caching {
    public class CachingPolicyService<T> : IPolicyService where T : IPolicyService {

        private readonly IPolicyService _inner;
        private readonly PolicyServerOptions _options;
        private readonly ICache<PolicyResult> _cache;
        private readonly ILogger _logger;

        public CachingPolicyService(
            PolicyServerOptions options,
            T inner,
            ICache<PolicyResult> cache,
            ILogger<CachingPolicyService<T>> logger
        ) {
            _options = options;
            _inner = inner;
            _cache = cache;
            _logger = logger;
        }

        public async Task<PolicyResult> EvaluateAsync(Policy policy, ClaimsPrincipal user) {
            if (policy.Hash.IsMissing()) { policy.CalculateHash(); }

            PolicyResult policyResult = await _cache.GetAsync(
                $"{policy.Hash}|||{user.Identity.Name}",
                _options.Caching.UserPolicyServiceExpiration,
                () => _inner.EvaluateAsync(policy, user),
                _logger
            );

            return policyResult;
        }
    }
}
