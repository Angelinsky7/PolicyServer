using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using PolicyServer1.Configuration;
using PolicyServer1.Extensions;
using PolicyServer1.ResponseHandling.Models;
using PolicyServer1.Stores;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.ResponseHandling.Default {
    public class DefaultPermissionRequestParser : IPermissionRequestParser {

        private readonly PolicyServerOptions _options;
        private readonly IClientStore _clientStore;
        private readonly ILogger _logger;

        public DefaultPermissionRequestParser(
            PolicyServerOptions options,
            IClientStore clientStore,
            ILogger<DefaultPermissionRequestParser> logger
        ) {
            _options = options;
            _clientStore = clientStore;
            _logger = logger;
        }

        public Task<PermissionRequest> ParseAsync(HttpContext context) {
            NameValueCollection query = context.Request.Query.AsNameValueCollection();
            String cliendIdFromQuery = query.Get("clientId");
            String clientId = context.User.FindFirst(_options.ClientIdentifier)?.Value;

            if (String.IsNullOrEmpty(clientId)) {
                return Task.FromResult(new PermissionRequest {
                    IsError = true,
                    Error = "clientId is mandatory"
                });
            }

            if(cliendIdFromQuery != clientId) {
                return Task.FromResult(new PermissionRequest {
                    IsError = true,
                    Error = "clientId from query and from token are not the same"
                });
            }

            _logger.LogDebug($"ClientId: {clientId}, User: {context.User}");

            return Task.FromResult(new PermissionRequest {
                IsError = false,
                ClientId = clientId,
                User = context.User
            });
        }
    }
}
