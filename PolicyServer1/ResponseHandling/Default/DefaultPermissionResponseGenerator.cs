using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PolicyServer1.Extensions;
using PolicyServer1.Models;
using PolicyServer1.ResponseHandling.Models;
using PolicyServer1.Services;
using PolicyServer1.Stores;

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
            Evaluation evaluation = await _evaluatorService.BuildEvaluationAsync(request);

            _logger.LogDebug($"EvaluatorRequest Cache: { Newtonsoft.Json.JsonConvert.SerializeObject(request.Cache)}");
            _logger.LogDebug($"EvaluatorRequest Result: { Newtonsoft.Json.JsonConvert.SerializeObject(request.EvaluatorResults)}");

            switch (permissionRequest.ResponseMode) {
                case PermissionRequestReponseMode.Decision: {
                        List<Boolean> granted = new List<Boolean>();
                        IEnumerable<IGrouping<String, EvaluationItem>> groupByResource = evaluation.Results.GroupBy(p => p.RsName);

                        foreach (PermissionResourceScopeItem requestPermission in request.PermissionResourceScopeItems) {
                            if (requestPermission.ResouceName != null && requestPermission.ScopeName == null) {
                                granted.Add(groupByResource.Any(p => p.Key == requestPermission.ResouceName));
                            }else if(requestPermission.ResouceName != null && requestPermission.ScopeName != null) {
                                granted.Add(groupByResource.Any(p => p.Key == requestPermission.ResouceName && p.Any(a => a.Scopes.Any(m => m == requestPermission.ScopeName))));
                            }else if(requestPermission.ResouceName == null && requestPermission.ScopeName != null) {
                                granted.Add(groupByResource.Any(p => p.Any(a => a.Scopes.Any(m => m == requestPermission.ScopeName))));
                            } else {
                                return new Dictionary<String, Object> {
                                    {"ErrorMessage", "Requested resource empty" }
                                };
                            }
                        }

                        Dictionary<String, Object> result = new Dictionary<String, Object> {
                            { nameof(client.ClientId),                      client.ClientId },
                            { nameof(evaluation.Results),                   granted.Count(p => !p) == 0 },
                            //{ nameof(evaluation.LastPolicyChangeDate),      evaluation.LastPolicyChangeDate },
                            //{ nameof(evaluation.Roles),                     evaluation.Roles },
                            //{ nameof(evaluation.Permissions),               evaluation.Permissions }
                        };

                        return result;
                    }
                case PermissionRequestReponseMode.Permissions: {
                        Dictionary<String, Object> result = new Dictionary<String, Object> {
                            { nameof(client.ClientId),                        client.ClientId },
                            { nameof(evaluation.Results),                     evaluation.Results.OnlyWithScopes() },
                            //{ nameof(evaluation.LastPolicyChangeDate),      evaluation.LastPolicyChangeDate },
                            //{ nameof(evaluation.Roles),                     evaluation.Roles },
                            //{ nameof(evaluation.Permissions),               evaluation.Permissions }
                        };

                        return result;
                    }
                case PermissionRequestReponseMode.Analyse: {
                        if (!client.Options.AnalyseModeEnabled) {
                            return new Dictionary<String, Object> {
                                {"ErrorMessage", "client does not permit Analyse. See the log." }
                            };
                        }

                        EvaluationAnalyse analyse = await _evaluatorService.BuildEvaluationAnalyseAsync(request);

                        Dictionary<String, Object> result = new Dictionary<String, Object> {
                             { nameof(analyse),                             analyse.Items },
                        };

                        return result;
                    }
            }

            return null;
        }

    }
}
