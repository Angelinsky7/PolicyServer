using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PolicyServer1.Endpoints.Results;
using PolicyServer1.Hosting;
using PolicyServer1.ResponseHandling;
using PolicyServer1.ResponseHandling.Models;

namespace PolicyServer1.Endpoints {

    [Obsolete]
    internal class PolicyEndpoint : IEndpointHandler {

        private readonly IPolicyRequestParser _requestParser;
        private readonly IPolicyResponseGenerator _responseGenerator;
        private readonly ILogger _logger;

        public PolicyEndpoint(
            IPolicyRequestParser requestParser,
            IPolicyResponseGenerator responseGenerator,
            ILogger<PolicyEndpoint> logger
        ) {
            _requestParser = requestParser;
            _responseGenerator = responseGenerator;
            _logger = logger;
        }

        public async Task<IEndpointResult> ProcessAsync(HttpContext context) {
            if (!HttpMethods.IsGet(context.Request.Method) && !HttpMethods.IsPost(context.Request.Method)) {
                _logger.LogWarning("Invalid HTTP method for policy endpoint.");
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }

            return await ProcessPolicyRequestAsync(context);
        }

        private async Task<IEndpointResult> ProcessPolicyRequestAsync(HttpContext context) {
            _logger.LogDebug("Start policy request");

            if (!context.User.Identity.IsAuthenticated) {
                _logger.LogWarning("User is not authorized.");
                return new StatusCodeResult(HttpStatusCode.Unauthorized);
            }

            PolicyRequest request = await _requestParser.ParseAsync(context);
            if (request.IsError) {
                return Error(request.Error, request.ErrorDescription);
            }

            Dictionary<String, Object> response = await _responseGenerator.ProcessAsync(request);

            _logger.LogDebug("End policy request");
            return new PolicyResult(response);
        }

        private IEndpointResult Error(String error, String description = null) {
            return new ErrorResult(error, description);
        }

    }
}
