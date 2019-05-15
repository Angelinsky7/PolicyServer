using Microsoft.AspNetCore.Http;
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
    public class DefaultPolicyRequestParser : IPolicyRequestParser {

        private readonly PolicyServerOptions _options;
        private readonly IClientStore _clientStore;

        public DefaultPolicyRequestParser(
            PolicyServerOptions options,
            IClientStore clientStore
        ) {
            _options = options;
            _clientStore = clientStore;
        }

        public Task<PolicyRequest> ParseAsync(HttpContext context) {
            NameValueCollection query = context.Request.Query.AsNameValueCollection();
            String clientId = context.User.FindFirst(_options.ClientIdentifier)?.Value;

            if (String.IsNullOrEmpty(clientId)) {
                return Task.FromResult(new PolicyRequest {
                    IsError = true,
                    Error = "clientId is mandatory"
                });
            }

            return Task.FromResult(new PolicyRequest {
                IsError = false,
                ClientId = clientId
            });
        }
    }
}
