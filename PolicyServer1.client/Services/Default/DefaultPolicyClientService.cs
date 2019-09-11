using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using PolicyServer1.Client.Configuration.Options;
using PolicyServer1.Extensions;
using PolicyServer1.Infrastructure;
using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Client.Services.Default {
    public class DefaultPolicyClientService : IPolicyClientService {

        private readonly PolicyClientOptions _options;

        public String Authority => _options.Authority;

        public DefaultPolicyClientService(
            PolicyClientOptions options
        ) {
            _options = options;
        }

        //public async Task<EvaluationTest> GetDecisionAsync(HttpContext context) {
        //    String accessToken = await context.GetTokenAsync("access_token");
        //    if (accessToken.IsMissing()) { throw new ArgumentNullException(nameof(accessToken)); }

        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //    String content = await client.GetStringAsync($"{_options.Authority}/{Constants.ProtocolRoutePaths.Permission}?clientId={_options.ClientId}&response_mode=decision");

        //    EvaluationTest result = ObjectSerializer.FromString<EvaluationTest>(content);

        //    return result;
        //}

        //public async Task<EvaluationResult> GetPermissionsAsync(HttpContext context) {
        //    String accessToken = await context.GetTokenAsync("access_token");
        //    if (accessToken.IsMissing()) { throw new ArgumentNullException(nameof(accessToken)); }
            
        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //    String content = await client.GetStringAsync($"{_options.Authority}/{Constants.ProtocolRoutePaths.Permission}?clientId={_options.ClientId}&response_mode=permissions");

        //    EvaluationResult result = ObjectSerializer.FromString<EvaluationResult>(content);

        //    return result;
        //}

        public Task<Boolean> HasPermissionAsync(ClaimsPrincipal user, String permissionName) {
            Boolean result = user.HasClaim(p => p.Type == Constants.Policy.Permission && p.Value == permissionName);
            return Task.FromResult(result);
        }

        public Task<Boolean> HasRoleAsync(ClaimsPrincipal user, String roleName) {
            Boolean result = user.HasClaim(p => p.Type == Constants.Policy.Role && p.Value == roleName);
            return Task.FromResult(result);
        }
    }
}
