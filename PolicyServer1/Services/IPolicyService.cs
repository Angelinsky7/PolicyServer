using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using PolicyServer1.Models;

namespace PolicyServer1.Services {

    [Obsolete]

    public interface IPolicyService {

        //Task<EvaluationResult> EvaluateResultAsync(String clientId, ClaimsPrincipal user, IEnumerable<String> permissions = null);
        //Task<EvaluationTest> EvaluateTestAsync(String clientId, ClaimsPrincipal user, IEnumerable<String> permissions = null);

        // Task<Boolean> HasPermissionAsync(ClaimsPrincipal user, String permission);
        // Task<Boolean> IsInRoleAsync(ClaimsPrincipal user, String role);
    }
}