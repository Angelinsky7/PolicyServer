using Microsoft.AspNetCore.Http;
using PolicyServer1.Extensions;
using PolicyServer1.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Endpoints.Results {
    internal class DiscoveryDocumentResult : IEndpointResult {

        public Dictionary<String, Object> Entries { get; private set; }
        public Nullable<Int32> MaxAge { get; private set; }

        public DiscoveryDocumentResult(Dictionary<String, Object> entries, Nullable<Int32> maxAge) {
            Entries = entries ?? throw new ArgumentNullException(nameof(entries)); ;
            MaxAge = maxAge;
        }
        public async Task ExecuteAsync(HttpContext context) {
            context.Response.SetNoCache(MaxAge ?? 0);
            await context.Response.WriteJsonAsync(Entries);
        }
    }
}
