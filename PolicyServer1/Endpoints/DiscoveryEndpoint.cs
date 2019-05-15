using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PolicyServer1.Configuration;
using PolicyServer1.Endpoints.Results;
using PolicyServer1.Extensions;
using PolicyServer1.Hosting;
using PolicyServer1.ResponseHandling;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Endpoints {
    public class DiscoveryEndpoint : IEndpointHandler {

        private readonly ILogger _logger;
        private readonly PolicyServerOptions _options;
        private readonly IDiscoveryResponseGenerator _responseGenerator;

        public DiscoveryEndpoint(
            PolicyServerOptions options,
            IDiscoveryResponseGenerator responseGenerator,
            ILogger<DiscoveryEndpoint> logger
        ) {
            _logger = logger;
            _options = options;
            _responseGenerator = responseGenerator;
        }

        public async Task<IEndpointResult> ProcessAsync(HttpContext context) {
            if (!HttpMethods.IsGet(context.Request.Method)) {
                _logger.LogWarning("Discovery endpoint only supports GET requests");
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }

            _logger.LogDebug("Start discovery request");

            if (!_options.Endpoints.EnableDiscoveryEndpoint) {
                _logger.LogInformation("Discovery endpoint disabled. 404.");
                return new StatusCodeResult(HttpStatusCode.NotFound);
            }

            String baseUrl = context.GetPolicyServerBaseUrl().EnsureTrailingSlash();
            String issuerUri = context.GetPolicyServerIssuerUri();
            Dictionary<String, Object> response = await _responseGenerator.CreateDiscoveryDocumentAsync(baseUrl, issuerUri);

            return new DiscoveryDocumentResult(response, _options.Discovery.ResponseCacheInterval);

        }
    }
}
