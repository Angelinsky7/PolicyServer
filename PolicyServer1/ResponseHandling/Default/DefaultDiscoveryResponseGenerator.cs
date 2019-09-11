using Microsoft.Extensions.Logging;
using PolicyServer1.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.ResponseHandling.Default {
    public class DefaultDiscoveryResponseGenerator : IDiscoveryResponseGenerator {

        protected readonly ILogger _logger;
        protected readonly PolicyServerOptions _options;

        public DefaultDiscoveryResponseGenerator(
            PolicyServerOptions options,
            ILogger<DefaultDiscoveryResponseGenerator> logger
        ) {
            _options = options;
            _logger = logger;
        }

        public Task<Dictionary<String, Object>> CreateDiscoveryDocumentAsync(String baseUrl, String issuerUri) {
            Dictionary<String, Object> entries = new Dictionary<String, Object>
            {
                { "Issuer", issuerUri }
            };

            if (_options.Discovery.ShowEndpoints) {
                if (_options.Endpoints.EnablePermissionEndpoint) {
                    entries.Add("PermissionEndpoint", baseUrl + Constants.ProtocolRoutePaths.Permission);
                }
                //if (_options.Endpoints.EnablePolicyEndpoint) {
                //    entries.Add("PolicyEndpoint", baseUrl + Constants.ProtocolRoutePaths.Policy);
                //}
            }

            return Task.FromResult(entries);
        }
    }
}
