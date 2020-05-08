using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using PolicyServer1.Client.Configuration.Options;
using PolicyServer1.Extensions;
using PolicyServer1.Infrastructure;
using PolicyServer1.Models;
using PolicyServer1.Models.Evalutation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Client.Services.Default {
    public class DefaultPolicyLocalService : IPolicyClientService {

        private readonly LocalServiceOption _options;

        public String Authority => "LOCAL_AUTHORITY";

        public DefaultPolicyLocalService(
            LocalServiceOption options
        ) {
            _options = options;
        }

        public Task<Boolean> HasPermissionAsync(ClaimsPrincipal user, String permissionName) {
            Boolean result = user.HasClaim(p => p.Type == Constants.Policy.Permission && p.Value == permissionName);
            return Task.FromResult(result);
        }
        public Task<Boolean> HasRoleAsync(ClaimsPrincipal user, String roleName) {
            Boolean result = user.HasClaim(p => p.Type == Constants.Policy.Role && p.Value == roleName);
            return Task.FromResult(result);
        }

        //public Task<EvaluationTest> GetDecisionAsync(HttpContext context) {
        //    //return await _options.Policy.EvaluateAsync(context.User);
        //    throw new NotImplementedException();
        //}

        public Task<PermissionResult> GetPermissionsAsync(HttpContext context) {
            //return await _options.Policy.EvaluateAsync(context.User);
            throw new NotImplementedException();
        }

    }
}
