using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PolicyServer1.Models;
using PolicyServer1.ResponseHandling.Models;
using PolicyServer1.Services;
using PolicyServer1.Stores;
using System.Linq;

namespace PolicyServer1.ResponseHandling.Default {
    public class DefaultPermissionResponseGenerator : IPermissionResponseGenerator {

        private readonly IClientStore _clientStore;
        private readonly IEvaluatorService _evaluatorService;
        private readonly ILogger _logger;

        public DefaultPermissionResponseGenerator(
            IClientStore clientStore,
            IEvaluatorService evaluatorService,
            ILogger<DefaultPermissionResponseGenerator> logger
        ) {
            _clientStore = clientStore;
            _evaluatorService = evaluatorService;
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

            EvaluatorRequest request = new EvaluatorRequest() {
                Client = client,
                User = permissionRequest.User,
                Permissions = permissionRequest.Permissions
            };

            await _evaluatorService.EvaluateAsync(request);
            Evaluation evaluation = await BuildResultEvaluationAsync(request);

            _logger.LogDebug($"EvaluatorRequest Cache: { Newtonsoft.Json.JsonConvert.SerializeObject(request.Cache)}");
            _logger.LogDebug($"EvaluatorRequest Result: { Newtonsoft.Json.JsonConvert.SerializeObject(request.Result)}");

            switch (permissionRequest.ResponseMode) {
                case PermissionRequestReponseMode.Decision: {
                        Dictionary<String, Object> result = new Dictionary<String, Object> {
                            //{ nameof(client.ClientId),                      client.ClientId },
                            // { nameof(evaluation.Result),                    evaluation.Result },
                            //{ nameof(evaluation.LastPolicyChangeDate),      evaluation.LastPolicyChangeDate },
                            //{ nameof(evaluation.Roles),                     evaluation.Roles },
                            //{ nameof(evaluation.Permissions),               evaluation.Permissions }
                        };

                        return result;
                    }
                case PermissionRequestReponseMode.Permissions: {

                        // Permission : "SomeOne(Policy) cando Something(Scope) on Resource(Resource)"

                        // EvaluationResult evaluation = await _evaluatorService.EvaluateResultAsync(permissionRequest.User, client, permissionRequest.Permissions);

                        // _logger.LogDebug($"EvaluationResult: { Newtonsoft.Json.JsonConvert.SerializeObject(evaluation)}");

                        Dictionary<String, Object> result = new Dictionary<String, Object> {
                            //{ nameof(client.ClientId),                        client.ClientId },
                            { nameof(evaluation.Results),                     evaluation.Results },
                            //{ nameof(evaluation.LastPolicyChangeDate),      evaluation.LastPolicyChangeDate },
                            //{ nameof(evaluation.Roles),                     evaluation.Roles },
                            //{ nameof(evaluation.Permissions),               evaluation.Permissions }
                        };

                        return result;
                    }
            }

            return null;
        }

        private async Task<Evaluation> BuildResultEvaluationAsync(EvaluatorRequest request) {
            Evaluation result = new Evaluation();
            Dictionary<Resource, List<Scope>> resourceByScopes = new Dictionary<Resource, List<Scope>>();

            foreach (KeyValuePair<Permission, PermissionDecision> permission in request.Result.PermissionsDecisions) {
                IEnumerable<Resource> resources = await _evaluatorService.GetResources(request, permission.Key);
                IEnumerable<Scope> scopes = await _evaluatorService.GetScopes(request, permission.Key);

                foreach (Resource resource in resources) {
                    if (!resourceByScopes.ContainsKey(resource)) {
                        resourceByScopes.Add(resource, new List<Scope>());
                    }
                    foreach (Scope scope in scopes) {
                        if (!resourceByScopes[resource].Contains(scope)) {
                            resourceByScopes[resource].Add(scope);
                            //TODO(demarco): It's missing the part of DENIED permission....
                        }

                    }
                }

            }

            _logger.LogDebug($"Ressouces by Scopes: { Newtonsoft.Json.JsonConvert.SerializeObject(resourceByScopes)}");

            foreach (KeyValuePair<Resource, List<Scope>> resourceByScope in resourceByScopes) {
                result.Results.Add(new EvaluationItem {
                    RsId = resourceByScope.Key.Id,
                    RsName = resourceByScope.Key.Name,
                    Scopes = resourceByScope.Value.Select(p => p.Name).ToList()
                });
            }

            return result;
        }

    }
}
