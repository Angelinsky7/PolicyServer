using Microsoft.AspNetCore.Http;
using PolicyServer1.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.Extensions;

namespace PolicyServer1.Hosting {
    public class BaseUrlMiddleware {
        private readonly RequestDelegate _next;
        private readonly PolicyServerOptions _options;

        public BaseUrlMiddleware(RequestDelegate next, PolicyServerOptions options) {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context) {
            var request = context.Request;

            if (_options.PublicOrigin.IsPresent()) {
                context.SetPolicyServerOrigin(_options.PublicOrigin);
            }

            context.SetIdentityServerBasePath(request.PathBase.Value.RemoveTrailingSlash());

            await _next(context);
        }
    }
}
