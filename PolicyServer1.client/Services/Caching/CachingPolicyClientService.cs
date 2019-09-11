using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PolicyServer1.Client.Configuration.Options;
using PolicyServer1.Extensions;
using PolicyServer1.Models;
using PolicyServer1.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Client.Services.Caching {
    public class CachingPolicyClientService<T> : IPolicyClientService where T : IPolicyClientService {

        //private readonly PolicyClientOptions _options;
        //private readonly T _inner;
        //private readonly ICache<PolicyResult> _cache;
        //private readonly ILogger _logger;

        //public String Authority => _inner.Authority;


        //public CachingPolicyClientService(
        //    PolicyClientOptions options,
        //    T inner,
        //    ICache<PolicyResult> cache,
        //    ILogger<CachingPolicyClientService<T>> logger
        //) {
        //    _options = options;
        //    _inner = inner;
        //    _cache = cache;
        //    _logger = logger;
        //}

        //public Task<Boolean> HasPermissionAsync(ClaimsPrincipal user, String permissionName) {
        //    return _inner.HasPermissionAsync(user, permissionName);
        //}
        //public Task<Boolean> HasRoleAsync(ClaimsPrincipal user, String roleName) {
        //    return _inner.HasRoleAsync(user, roleName);
        //}

        //public async Task<PolicyResult> GetPolicyAsync(HttpContext context) {
        //    PolicyResult result = await _cache.GetAsync(
        //        context.User.Identity.Name,
        //        _options.Caching.LocalUserPermissionExpiration,
        //        () => _inner.GetPolicyAsync(context),
        //        _logger
        //    );

        //    return result;
        //}
        public String Authority => throw new NotImplementedException();

        //public Task<EvaluationTest> GetDecisionAsync(HttpContext context) {
        //    throw new NotImplementedException();
        //}

        //public Task<EvaluationResult> GetPermissionsAsync(HttpContext context) {
        //    throw new NotImplementedException();
        //}

        //public Task<PolicyResult> GetPolicyAsync(HttpContext context) {
        //    throw new NotImplementedException();
        //}

        public Task<Boolean> HasPermissionAsync(ClaimsPrincipal user, String permissionName) {
            throw new NotImplementedException();
        }

        public Task<Boolean> HasRoleAsync(ClaimsPrincipal user, String roleName) {
            throw new NotImplementedException();
        }
    }
}
