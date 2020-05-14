using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PolicyServer1.Hosting {
    public class PolicyServerMiddleware {

        private readonly RequestDelegate _next;
        private readonly ILogger<PolicyServerMiddleware> _logger;

        public PolicyServerMiddleware(RequestDelegate next, ILogger<PolicyServerMiddleware> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IEndpointRouter router /*, IUserSession session, IEventService events*/) {
            try {
                IEndpointHandler endpoint = router.Find(context);
                if (endpoint != null) {
                    _logger.LogInformation("Invoking IdentityServer endpoint: {endpointType} for {url}", endpoint.GetType().FullName, context.Request.Path.ToString());

                    IEndpointResult result = await endpoint.ProcessAsync(context);

                    if (result != null) {
                        _logger.LogTrace("Invoking result: {type}", result.GetType().FullName);
                        await result.ExecuteAsync(context);
                    }

                    return;
                }
            } catch (Exception ex) {
                //await events.RaiseAsync(new UnhandledExceptionEvent(ex));
                _logger.LogCritical(ex, "Unhandled exception: {exception}", ex.Message);
                throw;
            }

            await _next(context);
        }

    }
}
