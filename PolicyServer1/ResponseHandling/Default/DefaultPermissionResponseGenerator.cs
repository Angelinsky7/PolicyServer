using Microsoft.Extensions.Logging;
using PolicyServer1.Configuration;
using PolicyServer1.Extensions;
using PolicyServer1.Models;
using PolicyServer1.ResponseHandling.Models;
using PolicyServer1.Services;
using PolicyServer1.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.ResponseHandling.Default {
    public class DefaultPermissionResponseGenerator : IPermissionResponseGenerator {

        private readonly IClientStore _clientStore;
        private readonly IPolicyService _policyService;
        private readonly ILogger _logger;

        public DefaultPermissionResponseGenerator(
            IClientStore clientStore,
            IPolicyService policyService,
            ILogger<DefaultPolicyResponseGenerator> logger
        ) {
            _clientStore = clientStore;
            _policyService = policyService;
            _logger = logger;
        }

        public async Task<Dictionary<String, Object>> ProcessAsync(PermissionRequest permissionRequest) {
            _logger.LogDebug("Creating permission response");

            Client client = await _clientStore.GetFromClientIdAsync(permissionRequest.ClientId);

            if (client == null) {
                _logger.LogWarning("No client found with the clientId given: {clienId}", permissionRequest.ClientId);

                return new Dictionary<String, Object> {
                    {"ErrorMessage", "client is not valid. See the log." }
                };
            }

            PolicyResult policyResult = await _policyService.EvaluateAsync(client.Policy, permissionRequest.User);

            _logger.LogDebug($"PolicyResult: { Newtonsoft.Json.JsonConvert.SerializeObject(policyResult)}");

            Dictionary<String, Object> result = new Dictionary<String, Object> {
                { nameof(client.ClientId),                      client.ClientId },              
                { nameof(policyResult.PolicyHash),              policyResult.PolicyHash },
                { nameof(policyResult.LastPolicyChangeDate),    policyResult.LastPolicyChangeDate },
                { nameof(policyResult.Roles),                   policyResult.Roles },
                { nameof(policyResult.Permissions),             policyResult.Permissions }
            };

            return result;
        }

    }
}
