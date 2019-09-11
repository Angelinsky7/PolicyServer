using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PolicyServer1.Models;
using PolicyServer1.ResponseHandling.Models;
using PolicyServer1.Stores;

namespace PolicyServer1.ResponseHandling.Default {

    [Obsolete]
    public class DefaultPolicyResponseGenerator : IPolicyResponseGenerator {

        private readonly IClientStore _clientStore;
        private readonly ILogger _logger;

        public DefaultPolicyResponseGenerator(
            IClientStore clientStore,
            ILogger<DefaultPolicyResponseGenerator> logger
        ) {
            _clientStore = clientStore;
            _logger = logger;
        }

        public async Task<Dictionary<String, Object>> ProcessAsync(PolicyRequest policyRequest) {
            _logger.LogDebug("Creating policy response");

            Client client = await _clientStore.GetFromClientIdAsync(policyRequest.ClientId);

            if (client == null) {
                _logger.LogWarning("No client found with the clientId given: {clienId}", policyRequest.ClientId);
            }
            //TODO(demarco): Rework this part please !
            //Dictionary<String, Object> result = new Dictionary<String, Object> {
            //    { "policy", client.Policy }
            //};

            //return result;
            //TODO(demarco): Rework this part please !

            return null;
        }
    }
}
