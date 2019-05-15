using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PolicyServer1.Configuration;
using PolicyServer1.Extensions;

namespace PolicyServer1.Hosting {
    public class EndpointRouter : IEndpointRouter {

        private readonly IEnumerable<Endpoint> _endpoints;
        private readonly PolicyServerOptions _options;
        private readonly ILogger _logger;

        public EndpointRouter(
            IEnumerable<Endpoint> endpoints,
            PolicyServerOptions options,
            ILogger<EndpointRouter> logger
        ) {
            _endpoints = endpoints;
            _options = options;
            _logger = logger;
        }

        public IEndpointHandler Find(HttpContext context) {
            if (context == null) throw new ArgumentNullException(nameof(context));

            foreach (Endpoint endpoint in _endpoints) {
                PathString path = endpoint.Path;
                if (context.Request.Path.Equals(path, StringComparison.OrdinalIgnoreCase)) {
                    String endpointName = endpoint.Name;
                    _logger.LogDebug("Request path {path} matched to endpoint type {endpoint}", context.Request.Path, endpointName);

                    return GetEndpointHandler(endpoint, context);
                }
            }

            _logger.LogTrace("No endpoint entry found for request path: {path}", context.Request.Path);

            return null;
        }

        private IEndpointHandler GetEndpointHandler(Endpoint endpoint, HttpContext context) {
            if (_options.Endpoints.IsEndpointEnabled(endpoint)) {
                if (context.RequestServices.GetService(endpoint.Handler) is IEndpointHandler handler) {
                    _logger.LogDebug("Endpoint enabled: {endpoint}, successfully created handler: {endpointHandler}", endpoint.Name, endpoint.Handler.FullName);
                    return handler;
                } else {
                    _logger.LogDebug("Endpoint enabled: {endpoint}, failed to create handler: {endpointHandler}", endpoint.Name, endpoint.Handler.FullName);
                }
            } else {
                _logger.LogWarning("Endpoint disabled: {endpoint}", endpoint.Name);
            }

            return null;
        }

    }
}
