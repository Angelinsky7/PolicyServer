using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;

namespace TestPolicyServer.Extensions {
    public static class PrincipalExtensions {

        [DebuggerStepThrough]
        public static string GetDisplayName(this ClaimsPrincipal principal) {
            var name = principal.Identity.Name;
            if (name.IsPresent()) return name;

            var sub = principal.FindFirst(JwtClaimTypes.Name);
            if (sub != null) return sub.Value;

            return string.Empty;
        }

    }
}
