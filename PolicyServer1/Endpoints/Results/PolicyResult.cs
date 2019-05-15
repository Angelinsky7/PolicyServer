using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PolicyServer1.Extensions;
using PolicyServer1.Hosting;

namespace PolicyServer1.Endpoints.Results {
    internal class PolicyResult : IEndpointResult {

        public Dictionary<String, Object> Clients { get; private set; }

        public PolicyResult(Dictionary<String, Object> clients) {
            Clients = clients ?? throw new ArgumentNullException(nameof(clients));
        }
        public async Task ExecuteAsync(HttpContext context) {
            context.Response.SetNoCache();
            await context.Response.WriteJsonAsync(Clients);
        }
    }
}
