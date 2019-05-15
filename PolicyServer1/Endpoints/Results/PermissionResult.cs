using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PolicyServer1.Extensions;
using PolicyServer1.Hosting;

namespace PolicyServer1.Endpoints.Results {
    internal class PermissionResult : IEndpointResult {

        public Dictionary<String, Object> Permissions { get; private set; }

        public PermissionResult(Dictionary<String, Object> permissions) {
            Permissions = permissions ?? throw new ArgumentNullException(nameof(permissions));
        }
        public async Task ExecuteAsync(HttpContext context) {
            context.Response.SetNoCache();
            await context.Response.WriteJsonAsync(Permissions);
        }
    }
}
