using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PolicyServer1.Endpoints.Results;
using PolicyServer1.Extensions;
using PolicyServer1.Hosting;
using PolicyServer1.ResponseHandling;
using PolicyServer1.ResponseHandling.Models;

namespace PolicyServer1.Endpoints {
    internal class PermissionEndpoint : IEndpointHandler {

        private readonly IPermissionRequestParser _requestParser;
        private readonly IPermissionResponseGenerator _responseGenerator;
        private readonly ILogger _logger;

        public PermissionEndpoint(
            IPermissionRequestParser requestParser,
            IPermissionResponseGenerator responseGenerator,
            ILogger<PolicyEndpoint> logger
        ) {
            _requestParser = requestParser;
            _responseGenerator = responseGenerator;
            _logger = logger;
        }

        public async Task<IEndpointResult> ProcessAsync(HttpContext context) {
            if (!HttpMethods.IsGet(context.Request.Method) && !HttpMethods.IsPost(context.Request.Method)) {
                _logger.LogWarning("Invalid HTTP method for permission endpoint.");
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }

            return await ProcessPolicyRequestAsync(context);
        }

        private async Task<IEndpointResult> ProcessPolicyRequestAsync(HttpContext context) {
            _logger.LogDebug("Start permission request");

            if (!context.User.Identity.IsAuthenticated) {
                _logger.LogWarning("User is not authorized.");
                return new StatusCodeResult(HttpStatusCode.Unauthorized);
            }

            PermissionRequest request = await _requestParser.ParseAsync(context);
            if (request.IsError) {
                return Error(request.Error, request.ErrorDescription);
            }

            Dictionary<String, Object> response = await _responseGenerator.ProcessAsync(request);

            String baseUrl = context.GetPolicyServerBaseUrl().EnsureTrailingSlash();
            String issuerUri = context.GetPolicyServerIssuerUri();
            response.Add("Issuer", issuerUri);
            response.Add("ExpireIn", 3600);
                       
            _logger.LogDebug("End permission request");
            return new PermissionResult(response);
        }

        private IEndpointResult Error(String error, String description = null) {
            return new ErrorResult(error, description);
        }

    }
}
